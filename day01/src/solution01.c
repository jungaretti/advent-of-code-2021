#include <limits.h>
#include <stdio.h>
#include <stdlib.h>

const int LINE_BUFFER_SIZE = 16;

int cntdpthbywndw(FILE *file, int windowSize)
{
    int solution = 0;

    int counter = 0;
    int prevSum = INT_MAX;
    int window[windowSize];
    char line[LINE_BUFFER_SIZE];
    while (fgets(line, LINE_BUFFER_SIZE, file) != NULL)
    {
        // Parse the current depth
        int current = atoi(line);

        // Add depth to sliding window
        int target = counter % windowSize;
        window[target] = current;
        counter++;

        // Make sure to fill all slots before checking
        if (counter >= windowSize)
        {
            int windowSum = 0;
            for (size_t i = 0; i < windowSize; i++)
            {
                windowSum += window[i];
            }

            if (windowSum > prevSum)
            {
                solution++;
            }
            prevSum = windowSum;
        }
    }

    return solution;
}

int main(int argc, char const *argv[])
{
    FILE *file = fopen(argv[1], "r");

    printf("Part One: %i\n", cntdpthbywndw(file, 1));
    rewind(file);
    printf("Part Two: %i\n", cntdpthbywndw(file, 3));

    return 0;
}
