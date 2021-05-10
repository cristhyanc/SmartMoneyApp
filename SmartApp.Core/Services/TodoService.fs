namespace SmartApp.Core.Services

open System
open SmartApp.Core.Entities
open System.Collections.Generic
open SmartApp.Core.Contract
open System.Threading.Tasks

type public TodoService(todoRepository: ITodoRepository) =
    interface ITodoService with
       
       member  this.GetAllTodos() =
                  async  {   let todoItems: List<SmartApp.Common.DTO.TodoItem > =new List<SmartApp.Common.DTO.TodoItem>()
                             let! data=  todoRepository.GetAllAsync()  |> Async.AwaitTask
                             for item in data do
                                    let newItem=new SmartApp.Common.DTO.TodoItem (Description=item.Description, Id=item.Id , DueDate=item.DueDate)
                                    todoItems.Add(newItem)
                             return Seq.cast todoItems
                          } |> Async.StartAsTask

        member this.GetTodo(id) =
            async {
                let! data=todoRepository.GetAsync(id) |> Async.AwaitTask
                if not <| obj.ReferenceEquals(data,null)
                    then return new SmartApp.Common.DTO.TodoItem (Description=data.Description, Id=data.Id , DueDate=data.DueDate)
                else
                   return null             
            } |> Async.StartAsTask

