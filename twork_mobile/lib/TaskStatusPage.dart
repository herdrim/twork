import 'dart:async';
import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:flutter/cupertino.dart';
import 'package:twork_mobile/TaskPage.dart';
import 'package:http/http.dart' as http;

class TeamTaskStatus {
  int taskStatusId;
  String taskStatusName;
 
  TeamTaskStatus({ this.taskStatusId, this.taskStatusName });
 
  factory TeamTaskStatus.fromJson(Map<String, dynamic> json) { 
    return TeamTaskStatus (
      taskStatusId: json['statusId'],
      taskStatusName: json['statusName']
    );
  }
}

class TaskStatuses{
  static Future<http.Response> getTeamTaskStatuses(String userLogin, String password, int teamId) async {
    var url =  'http://192.168.0.11:85/api/Task/GetTeamTaskStatuses';    
    return await http.post(url, body: json.encode({ 'UserLogin': userLogin, 'Password': password, 'TeamId': teamId }),
      headers: { "Accept": "application/json", "content-type": "application/json" });    
  }
  
  static Future<http.Response> updateTaskStatus(String userLogin, String password, int teamId, int taskId, int taskStatusId) async {
    var url =  'http://192.168.0.11:85/api/Task/ChangeTaskStatus';    
    return await http.post(url, body: json.encode({ 'UserLogin': userLogin, 'Password': password, 'TeamId': teamId, 'TaskId': taskId, 'TaskStatusId': taskStatusId }),
      headers: { "Accept": "application/json", "content-type": "application/json" });    
  }
}

class TaskStatusPage extends StatefulWidget {
  final String userLogin;
  final String password;
  final Task task;
  final int currentTaskStatus;
  final int teamId;
  
  TaskStatusPage({ this.userLogin, this.password, this.task, this.currentTaskStatus, this.teamId });
  createState() => TaskStatusPageState(userLogin: userLogin, password: password, task: task, currentTaskStatus: currentTaskStatus, teamId: teamId);
}

class TaskStatusPageState extends State {
  final String userLogin;
  final String password;
  final Task task;
  final int currentTaskStatus;
  final int teamId;
  int newTaskStatus;
  String statusName = "";

  TaskStatusPageState({ this.userLogin, this.password, this.task, this.currentTaskStatus, this.teamId });
   
  var teamTaskStatuses = new List<TeamTaskStatus>();

  _getTeamTaskStatuses() {
    TaskStatuses.getTeamTaskStatuses(userLogin, password, teamId).then((http.Response response) {
      setState(() {
       Iterable list = json.decode(response.body);
       teamTaskStatuses = list.map((model) => TeamTaskStatus.fromJson(model)).toList();
       statusName = teamTaskStatuses.firstWhere((t) => t.taskStatusId == newTaskStatus).taskStatusName;
      });
    });
  }

  initState() {
    super.initState();
    _getTeamTaskStatuses();
    newTaskStatus = currentTaskStatus;
  }

  dispose() {
    super.dispose();
  }
  
  Column buildDragTargetStatuses() {
    List<Widget> widgets = new List<Widget>();
    teamTaskStatuses.forEach((status){

      var dragTarget = new DragTarget<String>(builder: (context, candidateData, rejectedData){
          return Text(status.taskStatusName);
        },
        onWillAccept: (data){
          return true;
        },
        onAccept: (data){
          newTaskStatus = status.taskStatusId;
          setState(() {
           statusName = status.taskStatusName; 
          });
        },
      );      

      var container = new Container(
        width: 100.0,
        height: 100.0,
        color: Colors.green,
        child: dragTarget
      );
      
      widgets.add(container);
    });
    
    return Column(children: widgets);
  }

  GlobalKey<ScaffoldState> scaffoldKey = new GlobalKey();

  @override
  build(context) {
    return Scaffold(
      key: scaffoldKey,
      appBar: AppBar(
        title: Text("Task"),
      ),
      body: Column(
        children: <Widget>[
          Padding(
            padding: EdgeInsets.only(bottom: 10.0, top: 20.0),
            child: Text("Status: " + statusName, style: TextStyle(fontWeight: FontWeight.bold, fontSize: 15.0),),
          ),
          Padding(
            padding: EdgeInsets.only(bottom: 10.0, top: 20.0),
            child: Text("Task: ", style: TextStyle(fontSize: 12.0),),
          ),
          Padding(
            padding: EdgeInsets.only( bottom: 30.0),
            child: Center(
              child: Draggable<String>(
                childWhenDragging: Container(),
                data: task.taskTitle,              
                child: Center(
                  child: Text(task.taskTitle, style: TextStyle(fontWeight: FontWeight.bold, fontSize: 15.0),),
                ),
                feedback: Center(
                  child: Text(task.taskTitle),
                ),
              )
            ),
          ),

          buildDragTargetStatuses(),
        ],
      ),
      floatingActionButton: FloatingActionButton.extended(
        onPressed: () {
          TaskStatuses.updateTaskStatus(userLogin, password, teamId, task.taskId, newTaskStatus).then((http.Response response) {
            bool isSaved = json.decode(response.body);
          });
        },
        icon: Icon(Icons.save),
        label: Text("Save"),
      ),
    );
  }
  
}