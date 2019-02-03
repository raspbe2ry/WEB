<template>
  <div id="centralPart">
    <photo-shower v-for="trainer in trainers" 
                  v-bind:key=trainer.id
                  v-bind:trainer=trainer
                  @clicked="onClickChild">
    </photo-shower>
    <modal v-if="showModal"
                  @close="onModalClose"
                  v-bind:modalText="modalText">
    </modal>
  </div>
</template>

<script>
import PhotoShower from './PhotoShower'
import Modal from './Modal'
import axios from 'axios'

export default {
  name: 'Home',
  components: {
    PhotoShower,
    Modal
  }, 

  data () {
    return {
      showModal : false,
      trainers: [],
      modalText: { headerText : "", bodyText : "" }
    }
  },

  methods: {
    getPlayers: function(){
      return axios.get('http://localhost:3000/trainers')
        .then(response => {
          this.trainers=response.data
        })
    },
    onClickChild: function(val){
      this.showModal = true;
      return axios.get('http://localhost:3000/trainers/'+val)
        .then(response => {
          this.modalText.headerText=response.data.fullName;
          this.modalText.bodyText=response.data.description;
        });
    },
    onModalClose: function(){
      this.showModal = false;
    }
  },

  created: function(){
    this.getPlayers();
  }
}
</script>

<style scoped>
  @import './../Styles/Home.css';
</style>
