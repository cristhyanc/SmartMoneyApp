namespace SmartApp.Core.Contract

open SmartApp.Common.DTO
open System.Threading.Tasks


type public IExpiryngThingService =
    abstract GetAll: skip:int -> take:int -> Task<(int * seq<ExpiryngThingDto>)>

    abstract Get: id:int64 -> Task<ExpiryngThingDto>

    abstract Save: newItem:ExpiryngThingDto -> Task<ExpiryngThingDto>

