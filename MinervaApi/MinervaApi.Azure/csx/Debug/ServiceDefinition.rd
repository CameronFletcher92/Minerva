<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="MinervaApi.Azure" generation="1" functional="0" release="0" Id="815bbb37-59f0-4d78-9621-69faedadcb15" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="MinervaApi.AzureGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="MinervaApi:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/MinervaApi.Azure/MinervaApi.AzureGroup/LB:MinervaApi:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="MinervaApi:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/MinervaApi.Azure/MinervaApi.AzureGroup/MapMinervaApi:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="MinervaApiInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/MinervaApi.Azure/MinervaApi.AzureGroup/MapMinervaApiInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:MinervaApi:Endpoint1">
          <toPorts>
            <inPortMoniker name="/MinervaApi.Azure/MinervaApi.AzureGroup/MinervaApi/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapMinervaApi:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/MinervaApi.Azure/MinervaApi.AzureGroup/MinervaApi/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapMinervaApiInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/MinervaApi.Azure/MinervaApi.AzureGroup/MinervaApiInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="MinervaApi" generation="1" functional="0" release="0" software="C:\Users\Peter Gleeson\Documents\GitHub\Minerva\MinervaApi\MinervaApi.Azure\csx\Debug\roles\MinervaApi" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;MinervaApi&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;MinervaApi&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/MinervaApi.Azure/MinervaApi.AzureGroup/MinervaApiInstances" />
            <sCSPolicyUpdateDomainMoniker name="/MinervaApi.Azure/MinervaApi.AzureGroup/MinervaApiUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/MinervaApi.Azure/MinervaApi.AzureGroup/MinervaApiFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="MinervaApiUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="MinervaApiFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="MinervaApiInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="e8289125-8650-4125-a838-52e0fdbaa916" ref="Microsoft.RedDog.Contract\ServiceContract\MinervaApi.AzureContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="917abeab-79a4-432e-8dfe-05b1fd2d22d7" ref="Microsoft.RedDog.Contract\Interface\MinervaApi:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/MinervaApi.Azure/MinervaApi.AzureGroup/MinervaApi:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>