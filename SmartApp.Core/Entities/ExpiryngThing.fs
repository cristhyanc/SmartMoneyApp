namespace SmartApp.Core.Entities

open System.ComponentModel.DataAnnotations
open System
open System.ComponentModel.DataAnnotations.Schema


    [<CLIMutable>] 
    type ExpiryngThing =
        {
            [<Key>]
            [<DatabaseGenerated(DatabaseGeneratedOption.Identity)>]
            Id: int64

            [<Required>]
            Description: string
            ExpireDate:DateTime
            Renew:bool

            [<Required>]
            TenantId:Guid

            CreatedOn:DateTime
        }

