using System.Text;
using Xunit.Abstractions;

namespace Services.Test
{
    public class MatrixAreaFinderServiceTest
    {
        [Fact]
        public void Calculate_FindsThreeArea()
        {
            MatrixAreaFinderService matrixAreaFinder = new MatrixAreaFinderService("1,0,1;0,1,0");
            var result = matrixAreaFinder.Calculate();
            Assert.Equal(3, result);
        }
        [Fact]
        public void Calculate_FindsTwoArea()
        {
            MatrixAreaFinderService matrixAreaFinder = new MatrixAreaFinderService("1,0,1;1,1,0");
            var result = matrixAreaFinder.Calculate();
            Assert.Equal(2, result);
        }
        [Fact]
        public void Calculate_FindsOneArea()
        {
            MatrixAreaFinderService matrixAreaFinder = new MatrixAreaFinderService("1,1,1,0;0,1,0,0");
            var result = matrixAreaFinder.Calculate();
            Assert.Equal(1, result);

        }
        
        [Fact]
        public void Calculate_FindsSixArea()
        {
            var input = "0,1,1,0,0,0;0,0,0,0,1,1;0,0,0,0,0,0;1,1,1,0,0,0;0,0,0,0,0,1;1,1,0,1,0,1";
             
            MatrixAreaFinderService matrixAreaFinder = new MatrixAreaFinderService(input);
            var result = matrixAreaFinder.Calculate();

            Assert.Equal(6, result);

        }
        [Fact]
        public void Calculate_ThrowsExceptionIfItemsNotValid()
        {
            var exceptionType = typeof(System.ArgumentException);
            var expectedMessage = "Items can be only 0 or 1";
            var exception = Assert.Throws(exceptionType, () =>
             {
                 MatrixAreaFinderService matrixAreaFinder = new MatrixAreaFinderService("1,2,1,0;0,0,0");
             });
            Assert.Equal(expectedMessage, exception.Message);
        }
        [Fact]
        public void Calculate_ThrowsExceptionIfInputIsNotMatrix()
        {
            var exceptionType = typeof(System.ArgumentException);
            var expectedMessage = "Number of columns and rows must be equal";
            var exception = Assert.Throws(exceptionType, () =>
             {
                 MatrixAreaFinderService matrixAreaFinder = new MatrixAreaFinderService("1,1,1,0;0,0,0");
             });
            Assert.Equal(expectedMessage, exception.Message);
        }
        [Fact]
        public void Calculate_ThrowsExceptionIfMatrixLimitExceeded()
        {
            var exceptionType = typeof(System.ArgumentException);
            var expectedMessage = "Maximum number of rows or columns exceeded";

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 102; i++)
            {
                stringBuilder.Append("1,");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            var row = stringBuilder.ToString();
            stringBuilder.Append(";");
            stringBuilder.Append(row);

            var exception = Assert.Throws(exceptionType, () =>
            {
                MatrixAreaFinderService matrixAreaFinder = new MatrixAreaFinderService(stringBuilder.ToString());
            });
            Assert.Equal(expectedMessage, exception.Message);
        }
        public void Calculate_RunsUnderMaximumLimit()
        {

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                stringBuilder.Append("1,");
            }
            stringBuilder.Remove(stringBuilder.Length, 1);
            var row = stringBuilder.ToString();
            stringBuilder.Append(";");
            stringBuilder.Append(row);

            MatrixAreaFinderService matrixAreaFinder = new MatrixAreaFinderService(stringBuilder.ToString());
            var result = matrixAreaFinder.Calculate();
            Assert.Equal(1, result);
        }
    }
}