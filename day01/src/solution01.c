#include <limits.h>
#include <stdio.h>
#include <stdlib.h>

const int LINE_BUFFER_SIZE = 16;

int cntdpthbywndw(FILE *file, int windowSize)
{
    int solution = 0;

    int counted = 0;
    int prevSum = INT_MAX;
    int windowSum = 0;
    int window[windowSize];
    char line[LINE_BUFFER_SIZE];
    while (fgets(line, LINE_BUFFER_SIZE, file) != NULL)
    {
        // Evict the oldest element
        int target = counted % windowSize;
        int evicted = window[target];

        int current = atoi(line);
        window[target] = current;
        counted++;

        windowSum += current;
        if (counted >= windowSize)
        {
            windowSum -= evicted;
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
