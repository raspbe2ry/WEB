<template>
  <div>
      <full-calendar :events="trainingDates" class="test"/>
  </div>
</template>

<script>
import FullCalendar from 'vue-fullcalendar';
import axios from 'axios';

export default {
  name: 'trainings', 
  components: {
    FullCalendar
  },

  data() {
      return{
          trainingDates: []
      }
  },

  methods:{
    getTrainingDates: function(){
      let trainings;
      return axios.get('http://localhost:3000/trainings/')
        .then(response => {
          trainings=response.data;
          for (let index = 0; index < trainings.length; index++) {
            let cssClass;
            if(trainings[index].trainerId===1)
              cssClass="green";
            else if(trainings[index].trainerId===2)
              cssClass="red";
            else
              cssClass="blue";
            let training=
              {
                "title": trainings[index].title, 
                "start":trainings[index].start, 
                "end":trainings[index].end, 
                "cssClass":cssClass,
              };

            this.trainingDates.push(training);
          }
      });
    }
  },
  
  created: function(){
    this.getTrainingDates();
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}

.container > div:nth-child(1n) {
    background-color: #96ceb4;	
}

.container > div:nth-child(3n) {
    background-color: #88d8b0;
}

.container > div:nth-child(2n) {
    background-color: #ff6f69;
}

.container > div:nth-child(4n) {
    background-color: #ffcc5c;
}

/*--------------------------------------------*/

    .test{
      background: #71799C !important;      
    }
    .red {
      background: rgb(235, 77, 77) !important;
      color: whitesmoke !important;
    }
    .blue {
      background: rgb(59, 59, 163) !important;
      color: whitesmoke !important;
    }
    .orange {
      background: orange !important;
      color: white !important;
    }
    .green {
      background: rgb(49, 155, 49) !important;
      color: white !important;
    }
    .blue,
    .orange,
    .red,
    .green {
      font-size: 13px;
      font-weight: 500;
      text-transform: capitalize;
    }
    .event-item {
      padding: 2px 0 2px 4px !important;
    }

</style>