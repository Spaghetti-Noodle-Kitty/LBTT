# LBTT Documentation
#### Ligma Balls Telemetry Transfer

## What does LBTT do?
> LBTT attempts to implement a client/broker/sensor infrastructure.
> 
> You can think of LBTT like it is MQTT's "slower" bastard child it concieved whilst blackout drunk.
> 
> LBTT is supposed to be an alternative to MQTT but it is far from that, LBTT is mostly a pet-project of mine used for learning purposes on my part.

## Why did you create this?
> I was bored to hell and frustrated with MQTT's dev packages, so I wanted to learn more about MQTT and how it works.
> 
> Thus I created this fucked up amalgamation of code to just... learn a bit.

## Documentation
#### Usage
> LBTT is programmed fully in C#
> 
> The communication between the clients is handled through TCP and .Net.Sockets
>
> To use LBTT, you need to pass a few arguments to the .exe
> [IPAddress] Fairly self explanatory, sets which IP the local instance of LBTT should use.
> [Arguments] Here is where the actual selection of the role 
> * "-c" Sets the instances role to client
> 
> * "-b" Sets the instances role to broker
>
> * "-s" Sets the instances role to sensor

#### How it works
> For a functional setup, there needs to be atleast one client, one sensor and one broker
> 
> The LBTT infrastructure works by letting the sensors send messages to the broker, containing a hierarchy and the current value read by the sensor.
> 
> The hierarchy plays a key role, as clients can request a subscription to a hierarchy on the broker.
> If an update is received by the broker, it will check it's subscription list and get every client who is subscribed to the hierarchy of the update.
> The Broker will then send the updated value to all subscribed clients.
>
> The full update cycle would look something like this:<br/>
> ``` Client =[subto /x/y]> Broker```<br/>
> ``` Sensor =[ "Z"|/x/y ]> Broker```<br/>
> ``` Broker =[    "Z"   ]> Client```<br/>

#### WIP
> * Retain option (Broker caches updated value)
> * Last will (If sensor goes down, message will be broadcast to subscribers)
> * Real-World testing (This is a cluster fuck, god help me)
