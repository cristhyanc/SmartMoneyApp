namespace SmartApp.Core.Contract.ExpiringThings

open SmartApp.Common.Interfaces.Core
open SmartApp.Core.Entities.ExpiringThings

type public IExpiryngThingRepository =
    inherit IRepositoryBase<ExpiryngThing>