using System;

namespace noimportant
{
    class Program
    {
        /*
         * This method calculate the equation of a line.
         * We are trying to get the function to compare this data with excel data
         * We're using a code that we found in Google, the link it's below
         *
         * https://gist.github.com/NikolayIT/d86118a3a0cb3f5ed63d674a350d75f2
         *
         *  we're analizing the code to adapt it to our needs
         */
        static void Main(string[] args)
        {
            //Declared the array that container the data axis X with respect our data.
            var valoresX = new double[]
                              {
                                  1,2,3,4,5,6,7,8,9,10,11,12,13,14,15
                              };
            //Declared the array that container the data axis Y with respect our data.
            var valoresY = new double[]
                              {
                                  1,1,8,22,65,97,112,119,134,106,108,352,349,256,241
                              };
            //This variables are same to "R2", "B", "M"
            double rSquared, intercept, slope;
            //this method use the arrays store data
            // in this case 'out' makes mention of the variable to use in any part of the code
            LinearRegression(valoresX, valoresY, out rSquared, out intercept, out slope);
            
            //Call the variable rSquared that belong to the method LinearRegression
            Console.WriteLine($"R-squared = {rSquared}");
            //Call the variable intercept that belong to the method LinearRegression
            Console.WriteLine($"Intercept = {intercept}");
            //Call the variable slope that belong to the method LinearRegression
            Console.WriteLine($"Slope = {slope}");
            
            //Here we're going to do a prediction when 'x' takes the value: 5 and then we're going to comparing the result with the table data
            //we apply the function and substitute 5 instead of x
            var predictedValue = (slope * 5) + intercept;
            //Finally print the value on screen when x equal to 5 
            Console.WriteLine($"Prediction for 5: {predictedValue}");
        }
        //here start the method LinearRegression
        public static void LinearRegression(
            //these data are the values that we send through the invocation of method
            
            //arrays
            double[] xVals,
            double[] yVals,
            //variable 'r2'
            out double rSquared,
            //variable y 
            out double yIntercept,
            //variable m 
            out double slope)
        {
            // The first condition is that both arrays have the same length
            if (xVals.Length != yVals.Length)
            {
                //if the length is different, we print an error on the screen 
                throw new Exception("Input values should be with the same length.");    
            }
            // these variables are counts we use in the loop

            double sumOfX = 0;
            double sumOfY = 0;
            double sumOfXSq = 0;
            double sumOfYSq = 0;
            double sumCodeviates = 0;
            
            //The loop going to go through the array
            for (var i = 0; i < xVals.Length; i++)
            {
                //auxiliary loop variable
                var x = xVals[i];
                var y = yVals[i];
                sumCodeviates += x * y;
                sumOfX += x;
                sumOfY += y;
                sumOfXSq += x * x;
                sumOfYSq += y * y;
            }
            //save the number of elements in the array
            var count = xVals.Length;
            /*save the operation that we;re do in excel*/
            var ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
            var ssY = sumOfYSq - ((sumOfY * sumOfY) / count);
            
            var rNumerator = (count * sumCodeviates) - (sumOfX * sumOfY);
            var rDenom = (count * sumOfXSq - (sumOfX * sumOfX)) * (count * sumOfYSq - (sumOfY * sumOfY));
            var sCo = sumCodeviates - ((sumOfX * sumOfY) / count);
            /*save the mean of X and Y.
             
             */
            var meanX = sumOfX / count;
            var meanY = sumOfY / count;
            //we calculate the coefficient of determination
            var dblR = rNumerator / Math.Sqrt(rDenom);
            //square the coefficient
            rSquared = dblR * dblR;
            //y is equal to the average of Y minus the clearance of m
            yIntercept = meanY - ((sCo / ssX) * meanX);
            //change in y  / change in x
            slope = sCo / ssX;
        }
    
    }
}