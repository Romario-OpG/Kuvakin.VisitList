using FluentAssertions;
using System;
using System.Linq;
using WinFormsApp.Controls;
using Xunit;

namespace WinFormsApp.Tests.Controls
{
    public class ScheduleGridViewTests
    {
        [Fact]
        public void GetColumns_ShouldBeSuccess()
        {
            // setup
            var target = new ScheduleGridView(
                minRange: -1,
                maxRange: 1,
                currentDate: new DateTime(2021, 12, 31)
            );

            // act
            var result = target.GetColumns().ToArray();

            // assert
            result[0].Name.Should().Be("Id");
            result[0].HeaderText.Should().Be("id");

            result[1].Name.Should().Be("Фамилия Имя Отчество");
            result[1].HeaderText.Should().Be("full_name");

            result[2].Name.Should().Be("30.12.2021");
            result[2].HeaderText.Should().Be("2021-12-30");

            result[3].Name.Should().Be("31.12.2021");
            result[3].HeaderText.Should().Be("2021-12-31");

            result[4].Name.Should().Be("01.01.2022");
            result[4].HeaderText.Should().Be("2022-01-01");
        }
    }
}
