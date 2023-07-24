import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfirmationService, MessageService } from 'primeng/api';
import { InputTextModule } from 'primeng/inputtext';

import { CheckboxModule } from 'primeng/checkbox';
import { RadioButtonModule } from 'primeng/radiobutton';
import { RippleModule } from 'primeng/ripple';
import { ButtonModule } from 'primeng/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';


import { BrowserModule } from '@angular/platform-browser';
import { CardModule } from 'primeng/card';
import { MenuModule } from 'primeng/menu';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PasswordModule } from 'primeng/password';
import { DividerModule } from 'primeng/divider';
import { DialogModule } from 'primeng/dialog';
import { TooltipModule } from 'primeng/tooltip';
import { MultiSelectModule } from 'primeng/multiselect';
import { RatingModule } from 'primeng/rating';
import { TabViewModule } from 'primeng/tabview';
import { AccordionModule } from 'primeng/accordion';
import { PaginatorModule } from 'primeng/paginator';
import { FileUploadModule } from 'primeng/fileupload';
import { HttpClientModule } from '@angular/common/http';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { InputMaskModule } from 'primeng/inputmask';
import { DataViewModule } from 'primeng/dataview';
import { CalendarModule } from 'primeng/calendar';
import { TableModule } from 'primeng/table';
import { InputSwitchModule } from 'primeng/inputswitch';
import { StepsModule } from 'primeng/steps';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { PickListModule } from 'primeng/picklist'
import { ToolbarModule } from 'primeng/toolbar';
import { InputNumberModule } from 'primeng/inputnumber';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { TabMenuModule } from 'primeng/tabmenu';
import { DialogService, DynamicDialogModule } from 'primeng/dynamicdialog';
import { EditorModule } from 'primeng/editor'; 
import { ImageModule } from 'primeng/image'; 

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    InputTextModule, ButtonModule,  
    CheckboxModule, RadioButtonModule, ButtonModule, FormsModule, BrowserModule, RippleModule, DropdownModule
    , CardModule, MenuModule, PasswordModule, DividerModule, DialogModule, TooltipModule, MultiSelectModule, RatingModule,
    TabViewModule, AccordionModule, PaginatorModule, FileUploadModule, HttpClientModule, InputTextareaModule, InputMaskModule, DataViewModule, CalendarModule, TableModule,
    InputSwitchModule, StepsModule, MessagesModule, MessageModule, ProgressSpinnerModule, AutoCompleteModule, PickListModule, FormsModule, ReactiveFormsModule,
    ToolbarModule, TableModule, InputNumberModule, ConfirmDialogModule, ToastModule, TabMenuModule, DynamicDialogModule, EditorModule,ImageModule

  ],
  exports:
    [
      InputTextModule, ButtonModule, 
      CheckboxModule, RadioButtonModule, ButtonModule, FormsModule, BrowserModule, BrowserAnimationsModule, ReactiveFormsModule
      , RippleModule, DropdownModule, CardModule
      , MenuModule, PasswordModule, DividerModule, DialogModule, TooltipModule, MultiSelectModule, RatingModule, PickListModule,
      TabViewModule, AccordionModule, PaginatorModule, FileUploadModule, HttpClientModule, InputTextareaModule, InputMaskModule, DataViewModule,
      CalendarModule, TableModule, InputSwitchModule, StepsModule, MessagesModule, MessageModule, ProgressSpinnerModule, AutoCompleteModule, DynamicDialogModule, EditorModule,ImageModule,
      ToolbarModule, TableModule, InputNumberModule, ConfirmDialogModule, ToastModule, TabMenuModule
    ],
  providers: [MessageService, ConfirmationService, DialogService ]
})
export class SharedModule { }
