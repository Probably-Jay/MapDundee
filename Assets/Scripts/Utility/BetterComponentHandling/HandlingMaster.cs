using System;
using System.Collections.Generic;
using BetterComponentHandling.CleanAssignment;
using BetterComponentHandling.PropertyCaching;
using JetBrains.Annotations;
using UnityEngine;
using static BetterComponentHandling.NullChecks.NullChecks;

namespace BetterComponentHandling
{
    public static class HandlingMaster
    {
        [NotNull]
        public static T GetCachedComponent<T>(this MonoBehaviour monoBehaviour,
            [CanBeNull] ref T componentField,
            Func<T> getComponentFunction
         ) where T : class
        {
            return monoBehaviour.CustomGetCachedProperty(ref componentField, getComponentFunction);
        }   
        
        [NotNull]
        public static void GetComponentBetter<T>(this MonoBehaviour monoBehaviour,
            [CanBeNull] out T componentField,
            Func<T> getComponentFunction) where T : class
        {
            monoBehaviour.GetComponentAndAssignTo(out componentField, getComponentFunction);
        }  
        
      
        
    }

    public static class BetterAssignment2
    {
        private class Call<T> : IInitialCall<T> , IGetComponentFunction<T>
        {
            public MonoBehaviour Behaviour { get; set; }
            public Func<T> Function { get; set; }
        }

        public interface IInitialCall<T>
        {
            Func<T> Function { set; }
        } 
        public interface IGetComponentFunction<T>
        {
            Func<T> Function { get; }
        }
        
        public static IInitialCall<T> AcquireComponent<T>(this MonoBehaviour behaviour) where T : Component
        {
            return default;
        }

        public static IGetComponentFunction<T> WithFunction<T>(this IInitialCall<T> call,
            [NotNull] Func<T> getComponentFunction) where T : Component
        {
            return new Call<T>
            {
              // Behaviour = behaviour,
            };
        }
    }

    public static class BetterAssignemnt3
    {
        public interface IGetComponentFunction<T>
        {
            void AssignTo(out T componentField);
            void AssignToIfNull([CanBeNull] ref T componentField);
            T CacheAndReturnValue([CanBeNull] ref T componentField);
        }

        public interface IInitialCall<T>
        {
            IGetComponentFunction<T> WithFunction(
                [NotNull] Func<T> getComponentFunction);
            IGetComponentFunction<T> WithGetComponent();

        }
        
        private class Call<T> : IInitialCall<T> , IGetComponentFunction<T> where T : Component
        {
            private readonly MonoBehaviour behaviour;
            private Func<T> function;

            public Call(MonoBehaviour behaviour)
            {
                this.behaviour = behaviour;
                function = null;
            }

            public IGetComponentFunction<T> WithFunction([NotNull] Func<T> getComponentFunction)
            {
                function = getComponentFunction;
                return this;
            }

            public IGetComponentFunction<T> WithGetComponent()
            {
                function = behaviour.GetComponent<T>;
                return this;
            }
            
            public void AssignTo(out T componentField) 
                => AssignAndAssertNotNull(out componentField);

            public void AssignToIfNull([CanBeNull] ref T componentField) 
                => AssignValueIfNull(ref componentField);


            public T CacheAndReturnValue([CanBeNull] ref T componentField) 
                => AssignValueIfNull(ref componentField);

            private T AssignValueIfNull([NotNull] ref T componentField) 
                => IsNotNullUnity(componentField) 
                ? componentField 
                : AssignAndAssertNotNull(out componentField);

            private T AssignAndAssertNotNull(out T componentField) 
            {
                componentField = function();
                AssertNotNull(componentField);
                return componentField;
            }

         
        }
        
        public static IInitialCall<T> Get<T>(this MonoBehaviour behaviour) where T : Component
        {
            return new Call<T>(behaviour);
        }

       
        
    }

    public static class BetterAssignment
    {

       
        
        public interface IGetComponentFunctionStep<T> where T : Component
        {
         T AssignValue(out T componentField);
         T AssignValueIfNull(ref T componentField);
         Func<T> GetComponentFunction { get; }
        }

   

       

        [NotNull]
        public static IGetComponentFunctionStep<T> WithFunction<T>(this MonoBehaviour _,
            [NotNull] Func<T> getComponentFunction) where T : Component
            => new GetComponentFunctionStep<T>(getComponentFunction);


        

        public static void AssignTo<T>(this IGetComponentFunctionStep<T> componentValue,
            out T componentField) where T : Component
            => componentValue.AssignValue(out componentField);

        public static void AssignToIfNull<T>(this IGetComponentFunctionStep<T> componentValue,
            [CanBeNull] ref T componentField) where T : Component
            => componentValue.AssignValueIfNull(ref componentField);

        public static T CacheAndReturnValue<T>(this IGetComponentFunctionStep<T> componentValue,
            [CanBeNull] ref T componentField) where T : Component
            => componentValue.AssignValueIfNull(ref componentField);

        public static T ReturnValue<T>(this IGetComponentFunctionStep<T> componentValue) where T : Component 
            => componentValue.GetComponentFunction();


        private class GetComponentFunctionStep<T> :  IGetComponentFunctionStep<T> where T : Component
        {
            public Func<T> GetComponentFunction { get; }
            
            public GetComponentFunctionStep(Func<T> getComponentFunction)
            {
                GetComponentFunction = getComponentFunction;
            }

            public T AssignValue(out T componentField)
            {
                return AssignAndAssertNotNull(out componentField);
            }

            public T AssignValueIfNull([NotNull] ref T componentField)
            {
                return IsNotNullUnity(componentField) 
                    ? componentField 
                    : AssignAndAssertNotNull(out componentField);
            }

            private T AssignAndAssertNotNull(out T componentField)
            {
                componentField = GetComponentFunction();
                AssertNotNull(componentField);
                return componentField;
            }
        }
        
    }

  
}