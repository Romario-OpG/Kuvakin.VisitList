using FluentAssertions;
using System;
using System.Linq;
using WinFormsApp.Controls;
using Xunit;

namespace WinFormsApp.Tests.Controls
{
    public class ScheduleManagerTests
    {
        [Fact]
        public void GetColumns_ShouldBeSuccess()
        {
            // setup
            var target = new ScheduleManager(
                minRange: -1,
                maxRange: 1,
                currentDate: new DateTime(2021, 12, 31)
            );

            // act
            var result = target.GetColumns().ToArray();

            // assert
            result[0].Name.Should().Be("id");
            result[0].HeaderText.Should().Be("Id");

            result[1].Name.Should().Be("full_name");
            result[1].HeaderText.Should().Be("Фамилия Имя Отчество");

            result[2].Name.Should().Be("2021-12-30");
            result[2].HeaderText.Should().Be("30.12.2021");

            result[3].Name.Should().Be("2021-12-31");
            result[3].HeaderText.Should().Be("31.12.2021");

            result[4].Name.Should().Be("2022-01-01");
            result[4].HeaderText.Should().Be("01.01.2022");
        }
    }
}
