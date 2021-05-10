namespace SmartApp.Core.Entities

open System.ComponentModel.DataAnnotations
open System
open System.ComponentModel.DataAnnotations.Schema


    [<CLIMutable>] 
    type  TodoItem   =
        {
            [<Key>]
            [<DatabaseGenerated(DatabaseGeneratedOption.Identity)>]
            Id: int64
            Description: string
            DueDate: DateTime   
        }

