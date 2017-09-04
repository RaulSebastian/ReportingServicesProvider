using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack.DataAnnotations;

namespace ReportingServicesProvider.Infrastructure.Model
{
    public abstract class Entity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private readonly IDictionary<Type, IDomainEvent> _events =
            new ConcurrentDictionary<Type, IDomainEvent>();

        [Ignore, IgnoreDataMember]
        public IEnumerable<IDomainEvent> DomainEvents => _events.Values;


        public void RaiseEvent(IDomainEvent @event)
        {
            _events[@event.GetType()] = @event;
        }

        protected void ClearEvents()
        {
            _events.Clear();
        }
    }
}