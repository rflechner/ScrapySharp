#!/bin/bash

nuget Install FAKE -OutputDirectory packages -ExcludeVersion
mono packages/FAKE/tools/FAKE.exe $@ --fsiargs -d:MONO build.fsx
