namespace SmartApp.Core.Contract

open System.Threading.Tasks
open SmartApp.Core.Entities


type IRepositoryBase<'T> =
    
    abstract member Get: id:int64-> 'T

    abstract member GetAsync: id:int64 -> Task<'T>

    abstract member GetAll:unit -> seq<'T>

    abstract member GetAllAsync:unit -> Task<seq<'T>>

    