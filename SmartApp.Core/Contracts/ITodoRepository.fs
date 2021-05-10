namespace SmartApp.Core.Contract

open SmartApp.Core.Entities
open System.Collections.Generic


type public ITodoRepository =
    inherit IRepositoryBase<TodoItem>
