#include <limits.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

const size_t LINE_BUFFER_SIZE = 16;

int measure_movement(FILE *file)
{
    int horizontal = 0;
    int vertical = 0;
    char line[LINE_BUFFER_SIZE];
    while (fgets(line, LINE_BUFFER_SIZE, file) != NULL)
    {
        char *direction = strtok(line, " ");
        int vector = atoi(strtok(NULL, "\n"));

        if (direction[0] == 'u')
        {
            vertical -= vector;
        }
        else if (direction[0] == 'd')
        {
            vertical += vector;
        }
        else
        {
            // Who needs error handling?
            horizontal += vector;
        }
    }

    return horizontal * vertical;
}

int measure_movement_withaim(FILE *file)
{
    int horizontal = 0;
    int vertical = 0;
    int aim = 0;
    char line[LINE_BUFFER_SIZE];
    while (fgets(line, LINE_BUFFER_SIZE, file) != NULL)
    {
        char *direction = strtok(line, " ");
        int vector = atoi(strtok(NULL, "\n"));

        if (direction[0] == 'u')
        {
            aim -= vector;
        }
        else if (direction[0] == 'd')
        {
            aim += vector;
        }
        else
        {
            // Who needs error handling?
            horizontal += vector;
            vertical += vector * aim;
        }
    }

    return horizontal * vertical;
}

int main(int argc, char const *argv[])
{
    FILE *file = fopen(argv[1], "r");

    printf("Part One: %i\n", measure_movement(file));
    rewind(file);
    printf("Part Two: %i\n", measure_movement_withaim(file));

    return 0;
}
