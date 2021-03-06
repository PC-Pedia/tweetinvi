﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FakeItEasy;

namespace xUnitinvi.TestHelpers
{
    [ExcludeFromCodeCoverage]
    public class FakeRepository
    {
        private readonly Dictionary<object, object> _fakedRepository;

        public FakeRepository()
        {
            _fakedRepository = new Dictionary<object, object>();
        }

        public void ClearRepository()
        {
            _fakedRepository.Clear();
        }

        public void RegisterFake<T>(Fake<T> fake) where T : class
        {
            _fakedRepository.Add(fake.FakedObject, fake);
        }

        public void RegisterFake(object fake, object fakedObject)
        {
            _fakedRepository.Add(fakedObject, fake);
        }

        public Fake<T> GetFake<T>(T fakedObject) where T : class
        {
            object fake;
            if (_fakedRepository.TryGetValue(fakedObject, out fake))
            {
                if (fake is Fake<T>)
                {
                    return (Fake<T>)fake;
                }
            }

            return null;
        }
    }
}