namespace SmartApp.Core.Services.ExpiringThings

open System
open System.Collections.Generic
open SmartApp.Core.Contract
open System.Threading.Tasks
open MapsterMapper
open Mapster;
open SmartApp.Common.DTO
open SmartApp.Core.Contract.ExpiringThings
open SmartApp.Core.Entities.ExpiringThings


type public ExpiryngThingService(expiryngThingRepository: IExpiryngThingRepository, mapper: IMapper)  =
    interface IExpiryngThingService with
       
      
      member this.Delete(id: int64) : Task<unit> =
        async {
                  let! data=expiryngThingRepository.GetAsync(id) |> Async.AwaitTask
                  if not <| obj.ReferenceEquals(data,null)
                   then expiryngThingRepository.Delete(data) 
                        do! expiryngThingRepository.SaveChangesAsync() |> Async.AwaitTask |> Async.Ignore

              } |> Async.StartAsTask

      member this.GetAll(skip: int) (take: int): Task<int * seq<ExpiryngThingDto>> =   
                           async {
                                   let items: List<ExpiryngThingDto> = new List<ExpiryngThingDto>()
                                   let! (total, data) = expiryngThingRepository.GetAllAsync(skip,take) |> Async.AwaitTask
                                   let listData= data |> Seq.toList
                                   let result = listData.Adapt<List<SmartApp.Common.DTO.ExpiryngThingDto>>()
                                   return (total, Seq.cast result)
                                 } |>  Async.StartAsTask     

        member this.Get(id: int64): Task<ExpiryngThingDto> = 
            async {
                     let! data=expiryngThingRepository.GetAsync(id) |> Async.AwaitTask
                     if not <| obj.ReferenceEquals(data,null)
                        then return data.Adapt<ExpiryngThingDto>()
                     else
                        return null            
                  } |> Async.StartAsTask

        member this.Save(newItem:ExpiryngThingDto): Task<ExpiryngThingDto>=
            async {
                    if String.IsNullOrEmpty(newItem.Description)
                        then nullArg "Description" "Description is Require"
                    
                    let data=newItem.Adapt<ExpiryngThing>()

                    if data.Id=0L 
                        then do! expiryngThingRepository.Insert(data) |> Async.AwaitTask |> Async.Ignore
                    else  
                        expiryngThingRepository.Update(data)
                    
                    do! expiryngThingRepository.SaveChangesAsync() |> Async.AwaitTask |> Async.Ignore

                    return data.Adapt<ExpiryngThingDto>()

                  } |> Async.StartAsTask

        
       
