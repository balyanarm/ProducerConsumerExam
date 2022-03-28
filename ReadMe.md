# ProducerConsumerExam

It is a consumer-producer realization. Producer  save in a db tasks for the consumers, and each Consumer tries to take a new pending task and print the TaskText to the screen.See description in description.png

## How to run the project in Visual Studio
Choose Solution -> Properties -> Multiple startup projects
and choose ProducerConsumerExam.Consumer and ProducerConsumerExam.Producer.

Producer project automaticaly will start to generate tasks for Consumers. 

In Consumer Console Application you need to provide the Consumers count and the count of tasks Consumers should take every time and check the db for new tasks.
