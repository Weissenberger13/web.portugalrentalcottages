using System.Collections.Generic;
using System.Linq;
using BootstrapVillas.Content.Classes.CurrencyConverter;
using BootstrapVillas.Models;
using NUnit.Framework;
using Should;
using SpecsFor;

namespace Tests.PortugalVillas.CurrencyTests
{
    [TestFixture]
    public class CurrencyConverterFactorySpecs
    {
        [TestFixture]
        public class when_creating_a_new_currency_converter
        {
            private IEnumerable<CurrencyExchange> currencies;

            [SetUp]
            public void TestFixtureSetUp()
            {
                using (var db = new PortVillasContext())
                {
                    currencies = db.CurrencyExchanges.ToList();
                }

            }


            [Test]
            public void given_GBP_then_it_returns_the_same_amount()
            {
                var type = CurrencyEnum.GBP;
                var SUT = CurrencyConverterFactory.GetCurrencyConverter(type, currencies);

                var result = SUT.Convert(20);

                result.ShouldEqual(20);
                

            }

            [Test]
            public void given_USD_then_it_returns_a_higher_amount()
            {
                var type = CurrencyEnum.USD;
                var SUT = CurrencyConverterFactory.GetCurrencyConverter(type, currencies);

                var result = SUT.Convert(20);

                result.ShouldBeGreaterThan(20);
                result.ShouldNotEqual(20);
                result.ShouldNotBeInRange(0, 20);

                result.ShouldEqual(20 * SUT.ExchangeRate);
            }

            [TearDown]
            public void TearDown()
            {
                this.currencies = null;
            }

        }
    }
}