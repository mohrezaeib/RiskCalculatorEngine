# RiskCalculatorEngine
This Project aim to create a flexible, extendable and configurable rule engine that calculte AML risks. 
The rules could easily added or modified and the design is adopable to complex input Dto.
Based on values in input dto the risk is calculated and each risk score  along with final risk score is reported back.
Capabilities:
calculating risk for  custome ranges, for example x<Criteria A<y ==> Risk score Z
calculating risk for list of  custome ranges, for example calculating risk for list of Criteria A whith choice of different methods (such as averaging or maximum) look for ApplyOperator and enum Operation
Calculating Risk for Fixed string values
Calculating Risk for a list Fixed string values
Conditional Risk rules based on certain filter ==> for example if the input ContactType=="Personal" ==> use score and ratioes set A; otherwise use scores and ratio set B
Look more into FilterByConstantString


This Project is based on ABP.io template 
for running first run DbMigrator project and the HostAPI project
