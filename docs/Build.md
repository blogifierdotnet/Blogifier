To run build script, open PowerShell window and navigate to `/build` directory.
Run command:

```cmd
.\build.ps1
```

Build should clear `/build/publish` directory (if exists), compile and build projects, run tests 
and publish output to `/build/publish`.

### Testing
Published output is ready to be copied to host server. To verify, open command line (cmd utility), 
navigate to `/build/publish` and run:

```cmd
dotnet App.dll
```

This should start application at `http://localhost:5000/` so it can be tested locally
before uploading to host server.

### Demo option
Running build with `demo` flag will publish output with demo option turned on 
(password updates disabled).

```cmd
.\build.ps1 -ScriptArgs '-demo="true"'
```