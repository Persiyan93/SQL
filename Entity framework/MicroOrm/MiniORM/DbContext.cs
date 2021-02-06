using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace MiniORM
{
	public abstract class DbContext
    {
        private readonly DatabaseConnection connection;

        private readonly Dictionary<Type, PropertyInfo> dbsetProperties;

        internal static readonly Type[] AllowedSqlTypes =
        {

        };

        protected DbContext(string connectionString)
        {
            this.connection = new DatabaseConnection(connectionString);

            this.dbsetProperties = this.DiscoverDbSets();
            using (new ConnectionManager(connection))
            {
                this.InitializeDbSets();


            }
        }

        private void MapAllRelations()
        {
            foreach (var dbSetPropery in this.dbsetProperties)
            {
                var dbSetType = dbSetPropery.Key;
                var mapRelationsGeneric = typeof(DbContext)
                    .GetMethod("MapRelations", BindingFlags.Instance | BindingFlags.NonPublic)
                    .MakeGenericMethod(dbSetType);
                var dbSet = dbSetPropery.Value.GetValue(this);
                mapRelationsGeneric.Invoke(this, new[] { dbSet });

            }

        }

        private void MapRelations<T>(T dbSet)
            where T : class, new()
        {
            var entityType = typeof(T);

            

        }
        
        private void MapNavigationProperties<T>(DbSet<T> dbSet)
            where T:class,new()
        {
            var entityType = typeof(T);
            var foreignKeys = entityType.GetProperties()
                .Where(pr => pr.HasAttribute<ForeignKeyAttribute>())
                .ToArray();
            foreach (var foreignKey in foreignKeys)
            {
                var navigationPropertyName = foreignKey.GetCustomAttribute<ForeignKeyAttribute>().Name;

                var navigationProperty = entityType.GetProperty(navigationPropertyName);

                var navigationDbSet=this.dbsetProperties[navigationProperty.PropertyType].
            }

        }

        private Dictionary<Type,PropertyInfo> DiscoverDbSets()
        {
            var dbSets = this.GetType().GetProperties()
                .Where(pr => pr.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .ToDictionary(pr => pr.PropertyType.GetGenericArguments().First(), pr => pr);

            return dbSets;
        }

        private void InitializeDbSets()
        {
            foreach (var dbSet in this.dbsetProperties)
            {
                var dbSetType = dbSet.Key;
                var dbSetProperty = dbSet.Value;

                var populateDbSetGeneric = typeof(DbContext)
                    .GetMethod("PopulateDbSet", BindingFlags.Instance | BindingFlags.NonPublic)
                    .MakeGenericMethod(dbSetType);

                populateDbSetGeneric.Invoke(this, new object[] { dbSetProperty });
            }
        }

        private void PopulateDbSet<T>(PropertyInfo dbSet)
            where T :class ,new()
        {
            var entities = LoadTableEntities<T>();
            var dbSetInstance = new DbSet<T>(entities);
            ReflectionHelper.ReplaceBackingField(this, dbSet.Name, dbSetInstance);
        }

        private IEnumerable<T> LoadTableEntities<T>()
            where T: class
        {
            var table = typeof(T);

            var columns = GetEntityColumnNames(table);
            var tableName = GetTableName(table);
            var fetchedRows = this.connection.FetchResultSet<T>(tableName, columns).ToArray();
            return fetchedRows;
        }
        private string[] GetEntityColumnNames(Type table)
        {
            var tableName = this.GetTableName(table);
            var dbColumns = this.connection.FetchColumnNames(tableName);
            var columns = table.GetProperties()
                .Where(pr => dbColumns.Contains(pr.Name) &&
                                            !pr.HasAttribute<NotMappedAttribute>() &&
                                            AllowedSqlTypes.Contains(pr.PropertyType))
                .Select(pr => pr.Name)
                .ToArray();
            return columns;
        }
        private string GetTableName(Type table)
        {
            var tableName = ((TableAttribute)table
                .GetCustomAttributes(typeof(TableAttribute)).Single())?.Name;
            if (tableName==null)
            {
                tableName = this.dbsetProperties[table].Name;
            }
            return tableName;
        }
    }
}