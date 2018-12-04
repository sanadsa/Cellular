﻿using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TestCrm.UnitTests
{   
    public class MockDbSet<TEntity> : Mock<DbSet<TEntity>> where TEntity : class
    {
        public MockDbSet(List<TEntity> dataSource = null)
        {
            var data = (dataSource ?? new List<TEntity>());
            var queryable = data.AsQueryable();

            this.As<IQueryable<TEntity>>().Setup(e => e.Provider).Returns(queryable.Provider);
            this.As<IQueryable<TEntity>>().Setup(e => e.Expression).Returns(queryable.Expression);
            this.As<IQueryable<TEntity>>().Setup(e => e.ElementType).Returns(queryable.ElementType);
            this.As<IQueryable<TEntity>>().Setup(e => e.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            //Mocking the insertion of entities
            this.Setup(_ => _.Add(It.IsAny<TEntity>())).Returns((TEntity arg) => {
                data.Add(arg);
                return arg;
            });

            //...the same can be done for other members like Remove
        }
    }
}
