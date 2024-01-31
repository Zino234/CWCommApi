//STARTING CONNECTIONS HERE

//CHAT HUB
const chatHubConnection = new signalR.HubConnectionBuilder()
  .withUrl("/hub/chatHub")
  .build();

//GROUP HUB
const groupHubConnection = new signalR.HubConnectionBuilder()
  .withUrl("/hub/groupHub")
  .build();

  //Message HUB
  const messageHubConnection = new signalR.HubConnectionBuilder()
  .withUrl("/hub/messageHub")
  .build();
  
  //Message HUB
  const notificationHubConnection = new signalR.HubConnectionBuilder()
  .withUrl("/hub/notificationHub")
  .build();
  

  var hubConnectionId={};

  //STARTING ALL CONNECTIONS HERE 
startConnections(chatHubConnection,messageHubConnection,groupHubConnection,notificationHubConnection);



//THIS IS A FUNCTION FOR RECONNECTING AND RETAINING CONNECTIONS FOR A LIST OF CONNECTIONS
function retainConnection(connection){
    connection.connection.onclose=()=>{
      reconnect(connection);
      };
}
//FUNCTION FOR RETAINING CONNECTIONS ACROSS MULTIPLE CONNECTIONS ENDS HERE 






///THIS FUNCTION IS TO GET THE HUB NAME FROM THE BASR URI
function getHubNameFromConnection(connection){
  let hubUrlSpliting=connection.connection.baseUrl.split("/");
  var hubName=hubUrlSpliting[hubUrlSpliting.length-1];
  return hubName;
}
//FUNCTION ENDS HERE



//THIS FUCNTION HANDLES THE STARTING AND RETAINING OF CONNECTIONS ACROSS A LIST OF CONNECTIONS
function startConnections(...connections){
  connections.forEach(x=>{
    let hubName=getHubNameFromConnection(x)
    x.start().then(x=>{
        console.log("initial connection : "+hubName)
      // console.log("connectionId : "+x.connection.connectionId);
      console.log(`connection started : connected to ${hubName}`);
      // console.log(x) //the user connection id is coming soon
    })
    .catch(e=>console.error(e));
    retainConnection(x);


//THIS IS WHERE U CAN WRITE LOGIC FOR GENERAL  HUBS MESSAGES
    x.on("ReceiveConnectionId",(y)=>{
      hubConnectionId[`${getHubNameFromConnection(x)}ConnectionId`]=y;
      sessionStorage.setItem(`${getHubNameFromConnection(x)}ConnectionId`,y);
      // console.log(hubConnectionId);
    })
  })
}


//FUNCTION TO START AND RETAIN CONNECTION ENDS HERE

console.log(hubConnectionId);



// function showNoty(notificationMessage = "Notification", notificationType = "success") {
//   const defaultTimeout = 2000;

//   const validTypes = ['success', 'error', 'warning', 'info', 'alert'];
//   if (!validTypes.includes(notificationType)) {
//     notificationType = 'success';
//   }

//   new Noty({
//     type: notificationType,
//     text: notificationMessage,
//     timeout: defaultTimeout,
//     progressBar: true,
//     layout: 'topRight',
//     animation: {
//       open: 'animate__animated animate__fadeIn', // Use fadeIn for opening
//       close: 'animate__animated animate__fadeOut', // Use fadeOut for closing
//     },
//   }).show();
// }

// // Example usage:
// showNoty("This is an error notification", "error");
// showNoty("This is a warning notification", "warning");




// Example usage:
// showNoty("hello world")
// showNoty("This is an error notification", "error");
// showNoty("This is a warning notification", "warning");




// function showNoty(notificationMessage = "Notification", notificationType = "success") {
//   const defaultTimeout = 2000;

//   // Check if notificationType is valid, fallback to 'success' if not provided or invalid
//   const validTypes = ['success', 'error', 'warning', 'info'];
//   if (!validTypes.includes(notificationType)) {
//     notificationType = 'success';
//   }

//   // Create a new Noty instance with various customization options
//   new Noty({
//     type: notificationType,
//     text: notificationMessage,
//     timeout: false,
//     progressBar: true,
//   }).show();
// }



//THIS IS A FUNCTION TO RECONNECT A CONNNECTION UPON FAILURE OR DISCONNECTION FROM THE SERVER 
function reconnect(connection){
  setTimeout(() => {
    // showNoty(`you are  disconnected from ${getHubNameFromConnection(connection)}`,"error");
    // showNoty(`attempting to reconnect to ${getHubNameFromConnection(connection)}`,"warning");
    console.log(`you are  disconnected..\nattempting to reconnect to ${connection.connection.baseUrl} ...`);
      connection
        .start()
        .then((x) => {
          // console.log(`you are connected to ${connection.connection.baseUrl}`);
          console.log(`you are connected to ${getHubNameFromConnection(connection)}`);
        })
        .catch((x) => console.error(x))
        .finally(() => {
          if (connection.connection.connectionState !== 1) {
            setTimeout(() => {
              // console.log(`reattempting to reconnect  ${connection.connection.baseUrl} ...`);
              console.log(`reattempting to reconnect to ${getHubNameFromConnection(connection)}...`);
              connection
                .start()
                .then((x) => {
                  console.log(`you are connected to ${getHubNameFromConnection(connection)}`);
                  // alert("you are connected");
                
                // showNoty("Connected");
                
                  // Create a new Noty instance with various customization options
                  // new Noty({
                  //   type: 'info', // Set the notification type (info, success, error, warning, etc.)
                  //   text: 'Connected', // Set the notification text
                  //   layout: 'topCenter', // Position at the top-right corner (you can change to other layouts)
                  //   theme: 'sunset', // Use the 'metroui' theme (you can explore other themes)
                  //   progressBar: true, // Display a progress bar
                  //   progressBarColor: 'green', // Set the progress bar color
                  //   animation: {
                  //     open: 'animated fadeIn', // Specify the open animation
                  //     close: 'animated fadeOut', // Specify the close animation
                  //     speed: 500, // Set the animation speed
                  //   },
                  //   timeout: 3000, // Auto-close after 3 seconds
                  // }).show();
                
                
                })
                .catch((x) => console.error(x))
                .finally(() => {
                  if(connection.connection.ConnectionState!==1){
                    reconnect(connection);
                  }
                  console.log(connection.connection.connectionState);
                });
            }, 2000);
          }
          // console.log(connection.connection.connectionState);
        });
    }, 2000);
}
//RECONNECT FUNCTION ENDS HERE



































//////////////////////////////// CONFIGURATIONS END HERE ////////////////////////////////////////////////////////////


















///////////////////     FUNCTIONS FOR RECEIVAL OF UPDATES START HERE     ///////////////////////////////////////











groupHubConnection.on("NewGroupMessage",(x)=>{
  console.log("new message received")
  console.log(x);
})
















































////////// DO NOT CROSS /////////////////////////////////////////////////////////////////////////////////////////////////




















///PREVIOUS CODE 

// connection.connection.onclose = () => {
// //   while (connection.connection.connectionState !== 1) {
//     setTimeout(() => {
//     alert("you are  dicsonnected..\nattempting to reconnect...");
//       connection
//         .start()
//         .then((x) => {
//           alert("you are connected");
//         })
//         .catch((x) => console.error(x))
//         .finally(() => {
//           if (connection.connection.connectionState !== 1) {
//             setTimeout(() => {
//               alert("you are  dicsonnected..\nattempting to reconnect...");
//               connection
//                 .start()
//                 .then((x) => {
//                   alert("you are connected");
//                 })
//                 .catch((x) => console.error(x))
//                 .finally(() => {
//                   console.log(connection.connection.connectionState);
//                 });
//             }, 2000);
//           }
//           console.log(connection.connection.connectionState);
//         });
//     }, 2000);
// //   }
// };







// messageConnection.on("ReceiveConnectionId",(connectionId)=>{
//     sessionStorage.setItem("userMessageConnectionId",connectionId);
//     console.log("my connection id is ==>"+connectionId);
// })
// connection.on("ReceiveConnectionId",(connectionId)=>{
//     sessionStorage.setItem("userConnectionId",connectionId);
//     console.log("my connection id is ==>"+connectionId);
// })


// console.log(sessionStorage)



// setInterval(() => {
//   alert("you are  dicsonnected..\nattempting to reconnect...");
//     connection
//       .start()
//       .then((x) => {
//         alert("you are connected");
//       })
//       .catch((x) => console.error(x))
//       .finally(() => {
//         if (connection.connection.connectionState !== 1) {
//           setTimeout(() => {
//             console.log("you are  dicsonnected..\nattempting to reconnect...");
//             connection
//               .start()
//               .then((x) => {
//                 alert("you are connected");
//               })
//               .catch((x) => console.error(x))
//               .finally(() => {
//                 console.log(connection.connection.connectionState);
//               });
//           }, 2000);
//         }
//         console.log(connection.connection.connectionState);
//       });
//   }, 2000);






// console.log(connection);

// connection.on("YouDisconnected", () => {
//   alert("you are  dicsonnected..\nattempting to reconnect...");
//   setTimeout(() => {
//     connection
//       .start()
//       .then((x) => {
//         if(x.connection.connectionState===1){
//             alert("you are connected");
//         }
//       })
//       .catch((x) => console.error(x));
//   }, 2000);
//   console.log();
// });

// connection.on("NewUserConnected", (x) => {
//   alert("new user connected");
//   console.log(x);
// });
