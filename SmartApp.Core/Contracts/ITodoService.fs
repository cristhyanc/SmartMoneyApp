namespace SmartApp.Core.Contract

open SmartApp.Common.DTO
open System.Threading.Tasks

type public ITodoService =
    abstract GetAllTodos: skip:int -> take:int -> Task<seq<TodoItem>>

    abstract GetTodo: id:int64 -> Task<TodoItem>