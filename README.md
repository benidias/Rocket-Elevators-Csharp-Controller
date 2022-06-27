# Rocket-Elevators-Csharp-Controller
# Description

This program controls a Column of elevators.

It sends an elevator when a user presses a button on a floor and it takes
a user to its desired floor when a button is pressed from the inside ....

Elevator selection is based on Elevator location, status, and direction. the program chooses the Lift which is close to the place and which is preferably moving towards the floor where the user is. 

# Dependencies

To be able to try the program, you need ..

- to install .net framework on your computer 
- install al the extensions required for c#


# Usage

To use the program, you need to type dotnet test and see if your all the scenarios are passed

## Example

int scenarioNumber = 3;
Scenarios scenarios = new Scenarios();
scenarios.run(scenarioNumber);