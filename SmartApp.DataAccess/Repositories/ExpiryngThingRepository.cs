using SmartApp.Core.Contract;
using SmartApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.DataAccess.Repositories
{
    public class ExpiryngThingRepository : RepositoryBase<ExpiryngThing>, IExpiryngThingRepository
    {
        SmartAppContext _context;
        public ExpiryngThingRepository(SmartAppContext context) : base(context)
        {
            _context = context;
        }



        public override ExpiryngThing Get(Int64 id)
        {
            var data = _context.QueryFirstOrDefault<ExpiryngThing>
                (@"SELECT [Id]
                      ,[Description]
                      ,[ExpireDate]
                      ,[Renew]
                      ,[TenantId]
                      ,[CreatedOn
                  FROM [dbo].[ExpiryngThing]
                  WHERE Id=@Id",
                new
                {
                    Id = id
                });

            return data;
        }

        public override async Task<ExpiryngThing> GetAsync(Int64 id)
        {
            var data = await _context.QueryFirstOrDefaultAsync<ExpiryngThing>
                (@"SELECT [Id]
                      ,[Description]
                      ,[ExpireDate]
                      ,[Renew]
                      ,[TenantId]
                      ,[CreatedOn
                  FROM [dbo].[ExpiryngThing]
                  WHERE Id=@Id",
                new
                {
                    Id = id
                });

            return data;
        }

        public override IEnumerable<ExpiryngThing> GetAll(int skip=0, int take=20)
        {
            var data = _context.Query<ExpiryngThing>
                (@"SELECT [Id]
                      ,[Description]
                      ,[ExpireDate]
                      ,[Renew]
                      ,[TenantId]
                      ,[CreatedOn
                  FROM [dbo].[ExpiryngThing]
                  ORDER BY [ExpireDate]  
                  OFFSET @Skip ROWS
                  FETCH NEXT @Take ROWS ONLY",
                new
                {
                    Skip = (skip * take),
                    Take = take
                });

            return data;

        }

        public async override  Task<Tuple<int, IEnumerable<ExpiryngThing>>> GetAllAsync(int skip, int take)
        {

            var total = await _context.ExecuteScalarAsync<Int32>
               (@"SELECT  count(id)
                  FROM [dbo].[ExpiryngThing]"
              );

            var data = await _context.QueryAsync<ExpiryngThing>
                (@"SELECT [Id]
                      ,[Description]
                      ,[ExpireDate]
                      ,[Renew]
                      ,[TenantId]
                      ,[CreatedOn]
                  FROM [dbo].[ExpiryngThing]
                  ORDER BY [ExpireDate] 
                  OFFSET @Skip ROWS
                  FETCH NEXT @Take ROWS ONLY",
                new
                {
                    Skip = (skip * take),
                    Take = take
                });

            return new Tuple<int, IEnumerable<ExpiryngThing>>(total, data);

        }

    }
}
