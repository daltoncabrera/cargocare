﻿
namespace MSESG.CargoCare.Core
{
    /// <summary>
    /// This represents the service entity for values.
    /// </summary>
    public class ValueService : IValueService
    {
        private bool _disposed;

        /// <summary>
        /// Gets the list of values.
        /// </summary>
        /// <returns>Returns the <see cref="ValueResponse"/> object.</returns>
        public ValueResponse GetValues()
        {
            var values = new[] { "value1", "value2" };
            return new ValueResponse() { Values = values };
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }
    }
}