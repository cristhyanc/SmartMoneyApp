namespace SmartApp.Todo

open SmartApp.Common.Interfaces.Todo
open SmartApp.Common.DTO
open System.Collections.Generic
open System
open SmartApp.DataAccess
open System.Linq

 //= interface  ITodoProcess with  
type public TodoProcess(context: SmartAppContext)  = 
   interface ITodoProcess with

        member  this.GetTodos()=
            let todoItems: List<TodoItem > =new List<TodoItem>()
            for item in context.TodoItems.ToList() do
                let newItem=new TodoItem (Description=item.Description, Id=item.Id , DueDate=item.DueDate)
                todoItems.Add(newItem)

            //let item=new TodoItem (Description="Todo 1", Id=Guid.NewGuid(), DueDate=DateTime.Now);
            //todoItems.Add(item)
            //let item2=new TodoItem (Description="Todo 2", Id=Guid.NewGuid(), DueDate=DateTime.Now);
            //todoItems.Add(item2)
            //let item3=new TodoItem (Description="Todo 3", Id=Guid.NewGuid(), DueDate=DateTime.Now);
            //todoItems.Add(item3)
            //let item4=new TodoItem (Description="Todo 4", Id=Guid.NewGuid(), DueDate=DateTime.Now);
            //todoItems.Add(item4)
            
            Seq.cast todoItems