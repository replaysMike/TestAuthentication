#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 61203
EXPOSE 44395

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["TestAuthentication/TestAuthentication.csproj", "TestAuthentication/"]
RUN dotnet restore "TestAuthentication/TestAuthentication.csproj"
COPY . .
WORKDIR "/src/TestAuthentication"
RUN dotnet build "TestAuthentication.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TestAuthentication.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TestAuthentication.dll"]