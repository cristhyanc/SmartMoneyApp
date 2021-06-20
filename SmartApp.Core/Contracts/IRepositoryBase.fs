namespace SmartApp.Core.Contract

open System.Threading.Tasks



type IRepositoryBase<'T> =
    
    abstract member Get: id:int64-> 'T

    abstract member GetAsync: id:int64 -> Task<'T>

    abstract member GetAll: skip:int -> take:int -> seq<'T>

    abstract member GetAllAsync: skip:int -> take:int  -> Task<int * seq<'T>>


    