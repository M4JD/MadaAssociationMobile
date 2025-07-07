using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MadaAssociationMobile.Global
{
    public class ObservableObject : INotifyPropertyChanged
    {
        //
        // Summary:
        //     Provides access to the PropertyChanged event handler to derived classes.
        protected PropertyChangedEventHandler? PropertyChangedHandler => this.PropertyChanged;

        //
        // Summary:
        //     Occurs after a property value changes.
        public event PropertyChangedEventHandler? PropertyChanged;

        //
        // Summary:
        //     Raises the PropertyChanged event if needed.
        //
        // Parameters:
        //   propertyName:
        //     (optional) The name of the property that changed.
        //
        // Remarks:
        //     If the propertyName parameter does not correspond to an existing property on
        //     the current class, an exception is thrown in DEBUG configuration only.
        public virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //
        // Summary:
        //     Raises the PropertyChanged event if needed.
        //
        // Parameters:
        //   propertyExpression:
        //     An expression identifying the property that changed.
        //
        // Type parameters:
        //   T:
        //     The type of the property that changed.
        public virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (this.PropertyChanged != null)
            {
                string propertyName = GetPropertyName(propertyExpression);
                if (!string.IsNullOrEmpty(propertyName))
                {
                    RaisePropertyChanged(propertyName);
                }
            }
        }

        //
        // Summary:
        //     Extracts the name of a property from an expression.
        //
        // Parameters:
        //   propertyExpression:
        //     An expression returning the property's name.
        //
        // Type parameters:
        //   T:
        //     The type of the property.
        //
        // Returns:
        //     The name of the property returned by the expression.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     If the expression is null.
        //
        //   T:System.ArgumentException:
        //     If the expression does not represent a property.
        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            return ((((propertyExpression.Body as MemberExpression) ?? throw new ArgumentException("Invalid argument", "propertyExpression")).Member as PropertyInfo) ?? throw new ArgumentException("Argument is not a property", "propertyExpression")).Name;
        }

        //
        // Summary:
        //     Assigns a new value to the property. Then, raises the PropertyChanged event if
        //     needed.
        //
        // Parameters:
        //   propertyExpression:
        //     An expression identifying the property that changed.
        //
        //   field:
        //     The field storing the property's value.
        //
        //   newValue:
        //     The property's value after the change occurred.
        //
        // Type parameters:
        //   T:
        //     The type of the property that changed.
        //
        // Returns:
        //     True if the PropertyChanged event has been raised, false otherwise. The event
        //     is not raised if the old value is equal to the new value.
        protected bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            field = newValue;
            RaisePropertyChanged(propertyExpression);
            return true;
        }

        //
        // Summary:
        //     Assigns a new value to the property. Then, raises the PropertyChanged event if
        //     needed.
        //
        // Parameters:
        //   propertyName:
        //     The name of the property that changed.
        //
        //   field:
        //     The field storing the property's value.
        //
        //   newValue:
        //     The property's value after the change occurred.
        //
        // Type parameters:
        //   T:
        //     The type of the property that changed.
        //
        // Returns:
        //     True if the PropertyChanged event has been raised, false otherwise. The event
        //     is not raised if the old value is equal to the new value.
        protected bool Set<T>(string propertyName, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            field = newValue;
            RaisePropertyChanged(propertyName);
            return true;
        }

        //
        // Summary:
        //     Assigns a new value to the property. Then, raises the PropertyChanged event if
        //     needed.
        //
        // Parameters:
        //   field:
        //     The field storing the property's value.
        //
        //   newValue:
        //     The property's value after the change occurred.
        //
        //   propertyName:
        //     (optional) The name of the property that changed.
        //
        // Type parameters:
        //   T:
        //     The type of the property that changed.
        //
        // Returns:
        //     True if the PropertyChanged event has been raised, false otherwise. The event
        //     is not raised if the old value is equal to the new value.
        protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            return Set(propertyName, ref field, newValue);
        }
    }
}