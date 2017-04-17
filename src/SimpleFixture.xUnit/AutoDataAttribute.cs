﻿using SimpleFixture.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;
using System;

namespace SimpleFixture.xUnit
{
    /// <summary>
    /// Provide autogenerated data for xUnit
    /// </summary>
    public class AutoDataAttribute : DataAttribute
    {
        private readonly object[] _parameters;

        /// <summary>
        /// Create random data for XUnit tests
        /// </summary>
        /// <param name="parameters"></param>
        public AutoDataAttribute(params object[] parameters)
        {
            _parameters = parameters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testMethod"></param>
        /// <returns></returns>
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return AttributeHelper.GetData(testMethod, _parameters);
        }
    }        
}
