[![progress-banner](https://app.codecrafters.io/progress/redis/f2dbf57e-6b79-4e07-a24c-6a6833635d2d)](https://app.codecrafters.io/users/swpknl)

This is a starting point for C# solutions to the
["Build Your Own Redis" Challenge](https://codecrafters.io/challenges/redis).

In this challenge, you'll build a toy Redis clone that's capable of handling
basic commands like `PING`, `SET` and `GET`. Along the way we'll learn about
event loops, the Redis protocol and more.

**Note**: If you're viewing this repo on GitHub, head over to
[codecrafters.io](https://codecrafters.io) to try the challenge.

# Passing the first stage

The entry point for your Redis implementation is in `src/Server.cs`. Study and
uncomment the relevant code, and push your changes to pass the first stage:

```sh
git add .
git commit -m "pass 1st stage" # any msg
git push origin master
```

That's all!

# Stage 2 & beyond

Note: This section is for stages 2 and beyond.

1. Ensure you have `dotnet (6.0)` installed locally
1. Run `./spawn_redis_server.sh` to run your Redis server, which is implemented
   in `src/Server.cs`.
1. Commit your changes and run `git push origin master` to submit your solution
   to CodeCrafters. Test output will be streamed to your terminal.
