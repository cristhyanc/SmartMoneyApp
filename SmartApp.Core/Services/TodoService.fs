namespace SmartApp.Core.Services

//open System
//open SmartApp.Core.Entities
//open System.Collections.Generic
//open SmartApp.Core.Contract
//open System.Threading.Tasks
//open MapsterMapper
//open Mapster;

//type public TodoService(todoRepository: ITodoRepository, mapper: IMapper) =
//    interface ITodoService with
              
//       member  this.GetAllTodos(skip: int)( take: int): Task<seq<SmartApp.Common.DTO.TodoItem>> =
//                  async  {  
                         
//                             let todoItems: List<SmartApp.Common.DTO.TodoItem > =new List<SmartApp.Common.DTO.TodoItem>()
//                             let! data= todoRepository.GetAllAsync(skip)(take)  |> Async.AwaitTask
//                             let listData=data |> Seq.toList
//                             let result =listData.Adapt<List<SmartApp.Common.DTO.TodoItem>>()                          
//                             return Seq.cast result                         
//                          } |> Async.StartAsTask


//        member this.GetTodo(id) =
//            async {
//                let! data=todoRepository.GetAsync(id) |> Async.AwaitTask
//                if not <| obj.ReferenceEquals(data,null)
//                    then return new SmartApp.Common.DTO.TodoItem (Description=data.Description, Id=data.Id , DueDate=data.DueDate)
//                else
//                   return null             
//            } |> Async.StartAsTask

