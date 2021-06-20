namespace SmartApp.Core.Services

open System
open SmartApp.Core.Entities
open System.Collections.Generic
open SmartApp.Core.Contract
open System.Threading.Tasks
open MapsterMapper
open Mapster;
open SmartApp.Common.DTO


type public ExpiryngThingService(expiryngThingRepository: IExpiryngThingRepository, mapper: IMapper)  =
    interface IExpiryngThingService with
       
      
      member this.GetAll(skip: int) (take: int): Task<int * seq<ExpiryngThingDto>> =   
                           async {
                                   let items: List<ExpiryngThingDto> = new List<ExpiryngThingDto>()
                                   let! (total, data) = expiryngThingRepository.GetAllAsync(skip)(take) |> Async.AwaitTask
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

        
       
