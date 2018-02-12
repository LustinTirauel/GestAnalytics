# GestAnalytics
GestAnalytics is a tool for analyzing videos simultaneously especially for Gesture Elicitation Studies. It is developed in Unity3D.


1- To run it properly you should install Xvid Codec. You can find the latest version here: https://www.xvid.com/download/

2 - It is tested in Unity 3D 5.5.0f3.

3 - After opening the project in Unity, you can run scenes Anaylsis and Questionnaire. Questionnaire 1 and Test are for experimental purposes. 

4 - You can also run the builds under the Builds folder. They only work in Windows.


QUESTIONNAIRE 
Questionnaire module is a tool for automatizing the gesture recording and data collecting process in gesture elicitation studies. In this module you can see videos of the commands are playing sequentially. For each tasks participants record a gesture video and then answers some questions about this gesture they performed. Questionnaire module records each video to the folder of each participants and also export the answers of participants corresponding to each command.

ANALYSIS
Gesture elicitation studies usually focus on extracting gesture sets which are preferable by users. For placing the most preferable gestures, the agreement scores of gestures for one task are calculated. This can only be done by identifying the similar gestures by examining each video. In a 20-participant user test with 20 tasks, a researcher has to open 400 video files and annotate or take notes about each to a separate medium. In GestAnalytics, 20 videos which belong to a task can be monitored at the same time, ordered in a customized matrix grid and zoomed in and out to examine it closely. Moreover, researchers can unmute to sounds of any videos to listen to participants’ ideas if think-aloud protocol was applied during the experiment. Monitoring these videos simultaneously saves researchers time since they do not need to open each and every video file and more importantly create the opportunity to compare many videos at the same time. 

  -Taxonomical Tagging
Gesture elicitation studies, besides extracting gesture sets, can yield results such as gesture taxonomies. For taxonomizing a gesture set, each video needs to be tagged with the related taxonomy item. In GestAnalytics, tags can be added to the videos on-the-fly and when a taxonomy tag is added, it creates a slot in each video to be marked. In Figure 5, you can see two different videos which are tagged with different taxonomy items.  In this example, gesture in the Video A has the “static” item while the one in the Video B has the “dynamic” item. Still, all videos have inactive items, that can be activated easily with a single click. In this tagging system, researchers can tag videos quickly and double-check their coding easily by comparing it to other videos. For further operations, tagging information can be exported as a “.csv” file.

  -Filtering
GestAnalytics lets researchers examine the videos per task in the default mode. However, tagging for taxonomy creation is a process with a lot of back-and-forth to make sure that each video is tagged with the correct taxonomy item. It is a burdensome process to check each video file to verify the tags. However, GestAnalytics allows researchers to filter and view all videos belonging to a tag or multiple tags at the same time. In this way, it is easier to notice faulty information about a taxonomy item. Additionally, videos also can be filtered per participant for making examinations about a specific participant. 

  -How to use the analysis tool?
Navigation: R Mouse + Drag
Zoom: Scroll
S: Sound on for the hovered video
Alt + A: Show task and filtration panel
Alt + T: Show tags of the videos
