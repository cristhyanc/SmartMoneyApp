namespace SmartApp.Core.Contract

open SmartApp.Core.Entities


type public ITodoRepository =
    inherit IRepositoryBase<TodoItem>
