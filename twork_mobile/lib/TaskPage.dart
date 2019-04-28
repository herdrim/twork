import 'dart:async';
import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:twork_mobile/TaskStatusPage.dart';
 
class TaskStatus {
  int taskStatusId;
  String taskStatusName;
  List<Task> tasks;
 
  TaskStatus({ this.taskStatusId, this.taskStatusName, this.tasks });
 
  factory TaskStatus.fromJson(Map<String, dynamic> json) { 
    var list = json['tasks'] as List;
    List<Task> tasks = list.map((i) => Task.fromJson(i)).toList();
    return TaskStatus (
      taskStatusId: json['taskStatusId'],
      taskStatusName: json['taskStatusName'],
      tasks: tasks
    );
  }
}

class Task {
  int taskId;
  String taskTitle;
  String taskDescription;
  String deathline;
  String startTime;
  String endTime;

  Task ({ this.taskId, this.taskTitle, this.taskDescription, this.deathline, this.startTime, this.endTime });
 
  factory Task.fromJson(Map<String, dynamic> json) { 
    return Task(
      taskId: json['taskId'],
      taskTitle: json['taskTitle'],
      taskDescription: json['taskDescription'],
      deathline: json['deathline'],
      startTime: json['startTime'],
      endTime: json['endTime']
    );
  }
}

class TasksByStatuses{
  static Future<http.Response> getTasks(String userLogin, String password, int teamId) async {
    var url =  'http://192.168.0.11:85/api/Task/GetTasks';    
    return await http.post(url, body: json.encode({ 'UserLogin': userLogin, 'Password': password, 'TeamId': teamId }),
      headers: { "Accept": "application/json", "content-type": "application/json" });    
  }
}
 
class TaskPage extends StatefulWidget {
  final String userLogin;
  final String password;
  final int teamId;
  
  TaskPage({Key key, this.userLogin, this.password, this.teamId}) : super(key: key);
  @override
  createState() => TaskPageState(userLogin: userLogin, password: password, teamId: teamId);
}

class TaskPageState extends State {
  final String userLogin;
  final String password;
  final int teamId;

  TaskPageState ({ this.userLogin, this.password, this.teamId });

  var tasksStatuses = new List<TaskStatus>();

  List<Widget> _buildTaskByStatus (int index, int pos){
    List<Widget> widgets = new List<Widget>();
    widgets.add(
      Container(
        padding: const EdgeInsets.all(5.0),
        decoration: new BoxDecoration(
          border: Border( right: BorderSide(color: Colors.black))
        ),
        child: Text(tasksStatuses[index].tasks[pos].taskTitle),
      )
    );
    if (tasksStatuses[index].tasks[pos].startTime != null && tasksStatuses[index].tasks[pos].startTime != ""){
      widgets.add(
        Container(
          padding: const EdgeInsets.all(5.0),
          decoration: new BoxDecoration(
            border: Border( right: BorderSide(color: Colors.black))
          ),
          child: Text(tasksStatuses[index].tasks[pos].startTime),
        )
      );
    }
    if (tasksStatuses[index].tasks[pos].deathline != null && tasksStatuses[index].tasks[pos].deathline != ""){
      widgets.add(
        Container(
          padding: const EdgeInsets.all(5.0),
          decoration: new BoxDecoration(
            border: Border( right: BorderSide(color: Colors.black))
          ),
          child: Text(tasksStatuses[index].tasks[pos].deathline),
        )
      );   
    } 
    return widgets;              
  }

  _getTasks() {
    TasksByStatuses.getTasks(userLogin, password, teamId).then((http.Response response) {
      setState(() {
       Iterable list = json.decode(response.body);
       tasksStatuses = list.map((model) => TaskStatus.fromJson(model)).toList();
      });
    });
  }

  initState() {
    super.initState();
    _getTasks();
  }

  dispose() {
    super.dispose();
  }

    @override
  build(context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Tasks"),
      ),
      body: ListView.separated(
        separatorBuilder: (context, index) => Divider(
          color: Colors.black,
        ),
        itemCount: tasksStatuses.length,
        itemBuilder: (context, index) {
          return Padding(
            padding: EdgeInsets.all(10.0),
            child: Column(
              children: <Widget>[
                Text(
                  tasksStatuses[index].taskStatusName,
                  style: Theme.of(context).textTheme.body2,
                ),
                Container(
                  alignment: Alignment.centerLeft, 
                  height: 150, 
                  child: ListView.separated(
                    separatorBuilder: (context, i) => Divider(
                      color: Colors.black,
                    ),
                    itemCount: tasksStatuses[index].tasks.length,
                    itemBuilder: (context, pos){
                      return GestureDetector(
                        onTap: () {
                          Navigator.push(context, 
                            MaterialPageRoute(builder: (context) =>
                              TaskStatusPage(
                                userLogin: userLogin, 
                                password: password, 
                                task: tasksStatuses[index].tasks[pos],
                                currentTaskStatus: tasksStatuses[index].taskStatusId,
                                teamId: teamId
                              )
                            )
                          );
                        },
                        child: Row(                   
                          children: _buildTaskByStatus(index, pos),                          
                        ),
                      );
                    },
                  )
                ) 
              ],
            ),
          );         
        },
      ),
    );
  }
}