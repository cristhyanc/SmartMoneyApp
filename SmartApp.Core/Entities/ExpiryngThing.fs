namespace SmartApp.Core.Entities.ExpiringThings

open System.ComponentModel.DataAnnotations
open System
open System.ComponentModel.DataAnnotations.Schema
open SmartApp.Common.DataAccess

 
    type ExpiryngThing() =
         inherit EntityBase()
                                          
                 let mutable  _id:int64 = 0L
                 let mutable _description:string = ""                 
                 let mutable  _expireDate:DateTime =DateTime.MinValue
                 let mutable  _renew:bool=false 


                 [<DatabaseGenerated(DatabaseGeneratedOption.Identity)>] 
                 [<Required>]
                 [<Key>]
                 member this.Id  with get () = _id  and set (value:int64) = _id <- value
                                  
                 [<Required>]
                 [<StringLength(1000)>]              
                 member this.Description  with get () = _description  and set (value:string) = _description <- value    
                           
                
                 member this.ExpireDate  with get () = _expireDate  and set (value:DateTime) = _expireDate <- value