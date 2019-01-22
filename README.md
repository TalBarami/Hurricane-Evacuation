# Hurricane-Evacuation
## IAI191 assignments 1,2

### Environment simulator and agents for the Hurricane Evacuation Problem
Implementation of an environment simulator that runs a path optimization problem.
We are given a weighted graph, and the goal is (starting at a given vertex) to visit as many as possible out of a set of vertices, and reach a given goal vertex before a given deadline. However, unlike standard shortest path problems in graphs, which have easy known efficient solution methods (e.g. the Dijkstra algorithm), here the problem is that there are more than 2 vertices to visit, their order is not given, and even the number of visited vertices is not known in advance. This is a problem encountered in many real-world settings, such as when you are trying to evacuate people who are stuck at home with no transportation before the hurricane arrives. 

### Hurricane Evacuation problem environment
 The environment consists of a weighted unidrected graph. Each vertex may contain a number of people to be evacuated, or a hurricane shelter. An agent (evacuation vehicle) at a vertex automatically picks up all the people at this vertex just before starting the next move, unless the vertex contains a hurricane shelter, in which case everybody in the vehicle is dropped off at the shelter (goal). It is also possible for edges (roads) to be blocked, in this assignment we assume complete knowledge so w.l.o.g. all edges are initially unblocked.

An agent can only do no-op (taking 1 time unit) or traverse actions. The time for traverse actions is equal to: w(1+Kp), where w is the edge weight, p is the number of people in the vehicle, and K is a known global non-negative "slow-down" constant, determining how much the vehicle is slowed due to load. The action always succeeds, unless the time limit is breached.

The simulator keeps track of time, the number of actions done by each agent, and the total number of people successfully evacuated. 

the simulator run each agent in turn, performing the actions retured by the agents, and update the world accordingly. Additionally, the simulator is capable of displaying the state of the world after each step, with the appropriate state of the agents and their score.

Each agent program (a function) works as follows. The agent is called by the simulator, together with a set of observations. The agent returns a move to be carried out in the current world state. The agent is allowed to keep an internal state (for example, a computed optimal path, or anything else desired) if needed. The agents can observe the entire state of the world. 

### Part 0: Simple agents
- A human agent, i.e. print the state, read the next move from the user, and return it to the simulator. This is used for debugging and evaluating the program.
- A greedy agent, that works as follows: the agent should compute the shortest currently unblocked path to the next vertex with people to be rescued, or to a shelter if it is carrying people, and try to follow it. If there is no such path, do no-op.
- A vandal agent, that blocks roads. The vandal works as follows: it does V no-ops, and then blocks the lowest-cost edge adjacent to its current vertex (takes 1 time unit). Then it traverses a lowest-cost remaining edge, and this is repeated. Prefer the lowest-numbered node in case of ties. If there is no edge to block or traverse, do no-op. 

### Part 1: Search agents (using heuristic function)
- A greedy search agent, that picks the move with best immediate heuristic value to expand next.
- An agent using A* search, with the same heuristic.
- An agent using a simplified version of real-time A*. 

### Part 2: Game tree search agents
- Adversarial (zero sum game): each agent aims to maximize its own score minus the opposing agent's score.
- A semi-cooperative game: each agent tries to maximize its own score. The agent disregards the other agent score, except that ties are broken cooperatively.
- A fully cooperative both agents aim to maximize the sum of scores. 

Input example:
```
#V 4    ; number of vertices n in graph (from 1 to n)

#E 1 2 W1                 ; Edge from vertex 1 to vertex 2, weight 1
#E 3 4 W1                 ; Edge from vertex 3 to vertex 4, weight 1
#E 2 3 W1                 ; Edge from vertex 2 to vertex 3, weight 1
#E 1 3 W4                 ; Edge from vertex 1 to vertex 3, weight 4
#E 2 4 W5                 ; Edge from vertex 2 to vertex 4, weight 5
#V 2 P 1                  ; Vertex 2 initially contains 1 person to be rescued
#V 1 S                    ; Vertex 1 contains a hurricane shelter (a "goal vertex" - there may be more than one)
#V 4 P 2                  ; Vertex 4 initially contains 2 persons to be rescued
#D 10                     ; Deadline is at time 10
```
