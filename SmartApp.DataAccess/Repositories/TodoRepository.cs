using SmartApp.Core.Contract;
using SmartApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.DataAccess.Repositories
{
    public class TodoRepository: RepositoryBase<TodoItem>, ITodoRepository
    {
        SmartAppContext _context;
        public TodoRepository(SmartAppContext context): base(context)
        {
            _context = context;
        }



        public override TodoItem Get(Int64 id)
        {
            var data = _context.QueryFirstOrDefault<TodoItem>
                (@"SELECT [Id]
                      ,[Description]
                      ,[DueDate]
                  FROM [dbo].[TodoItems]
                  WHERE Id=@Id
                  ORDER BY [Description] DESC",
                new
                {
                    Id = id                   
                });

            return data;
        }

        public override async Task<TodoItem> GetAsync(Int64 id)
        {
            var data = await _context.QueryFirstOrDefaultAsync<TodoItem>
                (@"SELECT [Id]
                      ,[Description]
                      ,[DueDate]
                  FROM [dbo].[TodoItems]
                  WHERE Id=@Id
                  ORDER BY [Description] DESC",
                new
                {
                    Id = id
                });

            return data;
        }

        public override IEnumerable<TodoItem> GetAll(int skip = 0, int take = 20)
        {
            var data =  _context.Query<TodoItem>
                (@"SELECT [Id]
                      ,[Description]
                      ,[DueDate]
                  FROM [dbo].[TodoItems]
                  ORDER BY [Description] DESC
                  OFFSET @Skip ROWS
                  FETCH NEXT @Take ROWS ONLY",
                new
                {
                    Skip = skip,
                    Take = take
                });

            return data;

        }

        public async override Task<Tuple<int, IEnumerable<TodoItem>>> GetAllAsync(int skip = 0, int take = 20)
        {

            //var data = await _context.QueryAsync<TodoItem>
            //    (@"SELECT [Id]
            //          ,[Description]
            //          ,[DueDate]
            //      FROM [dbo].[TodoItems]
            //      ORDER BY [Description] DESC
            //      OFFSET @Skip ROWS
            //      FETCH NEXT @Take ROWS ONLY",
            //    new
            //    {
            //        Skip = skip,
            //        Take = take
            //    });

            //return data;
            return null;

        }
       
    }
}
