import { ChangeDetectorRef, Component } from '@angular/core';
import { TaskResponse } from '../../../application/dtos/response/task-response';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { TaskRepository } from '../../../infrastructure/adapters/task-repository';
import { TaskRequest } from '../../../application/dtos/request/task-request.model';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent {

  listTask: TaskResponse[] = [];
  taskForm!: FormGroup;
  actionEditarCrud: boolean = false;
  minDate: Date = new Date();
  showTask: boolean = false;
  filtroId!: number | null;


  constructor(
    private fb: FormBuilder,
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private router: Router,
    private cdr: ChangeDetectorRef,
    private taskRepository: TaskRepository
  ) {
    this.createForm();
  }


  reloadPage() {
    const currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]).then(() => {
        this.cdr.detectChanges(); // Asegura la actualización de la vista
      });
    });
  }

  createForm(): void {
    this.taskForm = this.fb.group({
      id: [null],
      title: ['', Validators.required],
      description: ['', Validators.required],
      dueDate: [null, Validators.required],
      status: ['', Validators.required],
      //state: ['', Validators.required]
    });
  }

  cargarModalAgregar() {
    this.actionEditarCrud = false;
    this.taskForm.reset();
    this.showTask = true;
  }

  cargarModalEditar(task: TaskResponse): void {
    this.actionEditarCrud = true;
    this.taskForm.reset();
    this.taskForm.patchValue({
      id: task.id,
      title: task.title,
      description: task.description,
      dueDate: task.dueDate ? new Date(task.dueDate) : null,
      status: task.status,
      state: task.state,
      createdAt: task.createdAt,
      updatedAt: task.updatedAt
    });
    this.showTask = true;
  }

  getStatusSeverity(state: string): string {
    switch (state) {
      case 'PENDIENTE': return 'danger';
      case 'EN_PROGRESO': return 'warning';
      case 'COMPLETADO': return 'success';
      default: return 'info';
    }
  }


  async ngOnInit() {
    await this.getTasks();
    //this.loadTasks();
  }

  async onSubmit() {
    if (this.taskForm.invalid) return;
    const taskData: TaskRequest = {
      Id: this.taskForm.get('id')?.value ?? 0,
      Title: this.taskForm.get('title')?.value,
      Description: this.taskForm.get('description')?.value,
      Status: this.taskForm.get('status')?.value,
      state: "A",
      CreatedAt: this.taskForm.get('createdAt')?.value || new Date(),
      UpdatedAt: new Date(),
      due_date: this.taskForm.get('dueDate')?.value
    };
    if (this.actionEditarCrud) {
      console.log("json update task : " + JSON.stringify(taskData))
      const id = taskData?.Id ?? 0;
      await this.updateTask(id, taskData);
    } else {
      console.log("json create task : " + JSON.stringify(taskData))
      await this.createTask(taskData);
    }
  }


  async eliminarTarea(event: Event, task: TaskResponse) {
    this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: `¿Estás seguro de eliminar la tarea "${task.title}"?`,
      header: 'Confirmar eliminación',
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass: "p-button-danger p-button-text",
      rejectButtonStyleClass: "p-button-text p-button-text",
      acceptIcon: "none",
      rejectIcon: "none",
      accept: async () => {
        if (task.id) {
          await this.deleteTask(task.id);
        }
      },
      reject: () => {
        /*this.messageService.add({ 
          severity: 'info', 
          summary: 'Rechazado', 
          detail: 'Operación cancelada' 
        });*/
      }
    });
  }



  /*  =============== CONSUMO DE APIS ======================= */

  async getTasks(): Promise<void> {
    try {
      this.filtroId = null;
      const response = await this.taskRepository.getAllTask();
      if (!response.data) {
        this.messageService.add({
          severity: 'warn', summary: 'Warning',
          detail: 'No se encontraron tareas'
        });
        this.listTask = [];
        return;
      }
      this.listTask = response.data;
      this.reloadPage()
    } catch (error: any) {
      console.error('Error al obtener tareas:', error);
      const errorCode = error.error?.error?.code || error.error?.meta?.statusCode || 'Error';
      const errorDescription = error.error?.error?.description || error.error?.meta?.message || 'Error desconocido';
      this.messageService.add({
        severity: 'error',
        summary: errorCode,
        detail: errorDescription
      });
      this.listTask = [];
    }
  }

  async getByIdTasks(): Promise<void> {
    if (!this.filtroId) {
      await this.getTasks(); // Cargar todas las tareas si no hay ID
      return;
    }
    
    try {
      const response = await this.taskRepository.getById(this.filtroId);
      
      if (!response.data) {
        this.messageService.add({
          severity: 'info', 
          summary: 'Información', 
          detail: `No se encontró ninguna tarea con ID ${this.filtroId}`
        });
        await this.getTasks(); // Volver a cargar todas las tareas
        return;
      }
      
      // Convertir la respuesta individual a un array para la tabla
      this.listTask = [response.data];
      
    } catch (error: any) {
      console.error('Error al buscar tarea por ID:', error);
      const errorCode = error.error?.error?.code || 'Error';
      const errorDescription = error.error?.error?.description || 'Error al buscar la tarea';
      
      this.messageService.add({
        severity: 'error',
        summary: errorCode,
        detail: errorDescription
      });
      
      await this.getTasks(); // Cargar todas las tareas en caso de error
    }
  }

  async createTask(task: TaskRequest): Promise<void> {
    try {
      const response = await this.taskRepository.insert(task);
      if (!response.data) {
        this.messageService.add({
          severity: 'error',
          summary: response.error?.code,
          detail: response.error?.description
        });
        return;
      }
      this.messageService.add({
        severity: 'success',
        summary: 'Éxito',
        detail: `Task Creada Con Éxito "${task.Title}"`
      });
      await this.ngOnInit();
      this.reloadPage();
      this.showTask = false;
    } catch (error: any) {
      console.error('Error al crear la Task:', error);
      const errorCode = error.error?.error?.code || error.error?.meta?.statusCode || 'Error';
      const errorDescription = error.error?.error?.description || error.error?.meta?.message || 'Error desconocido';
      this.messageService.add({
        severity: 'error',
        summary: errorCode,
        detail: errorDescription
      });
    }
  }

  async updateTask(id: number, task: TaskRequest): Promise<void> {
    try {
      const response = await this.taskRepository.update(id, task);
      if (!response.data) {
        this.messageService.add({
          severity: 'error',
          summary: response.error?.code,
          detail: response.error?.description
        });
        return;
      }
      this.messageService.add({
        severity: 'success',
        summary: 'Éxito',
        detail: `Task Actualizada Con Éxito "${task.Title}"`
      });
      await this.ngOnInit();
      this.reloadPage();
      this.showTask = false;
    } catch (error: any) {
      console.error('Error al actualizar la Task:', error);
      const errorCode = error.error?.error?.code || error.error?.meta?.statusCode || 'Error';
      const errorDescription = error.error?.error?.description || error.error?.meta?.message || 'Error desconocido';

      this.messageService.add({
        severity: 'error',
        summary: errorCode,
        detail: errorDescription
      });
    }
  }

  async deleteTask(id: number): Promise<void> {
    try {
      const response = await this.taskRepository.inactivateById(id);
      if (!response.data) {
        this.messageService.add({
          severity: 'error',
          summary: response.error?.code,
          detail: response.error?.description
        });
        return;
      }

      this.messageService.add({
        severity: 'success',
        summary: 'Éxito',
        detail: `Task Borrada Con Éxito`
      });
      await this.ngOnInit();
      this.reloadPage();
    } catch (error: any) {
      console.error('Error al crear la Task:', error);

      // Obtener información de error independientemente de su estructura
      const errorCode = error.error?.error?.code || error.error?.meta?.statusCode || 'Error';
      const errorDescription = error.error?.error?.description || error.error?.meta?.message || 'Error desconocido';

      this.messageService.add({
        severity: 'error',
        summary: errorCode,
        detail: errorDescription
      });
    }
  }




}
