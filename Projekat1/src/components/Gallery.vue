<template>
  <div>
    <img class="image smallBase" v-for="(image, i) in photoSources" :src="image" :key="i" @click="index = i">
    <vue-gallery-slideshow :images="photoSources" :index="index" @close="index = null"></vue-gallery-slideshow>
  </div>
</template>

<script>
import VueGallerySlideshow from 'vue-gallery-slideshow';
import axios from 'axios';

export default {
  name: 'gallery', 
  components: {
        VueGallerySlideshow
  },

  data() {
      return{
          photoSources: [],
          index: null
      }
  },

  methods:{
      getPhotos: function(){
        
       return axios.get('https://api.flickr.com/services/rest/?method=flickr.photosets.getPhotos&api_key=1c64ed9b207e7c83702fb80197123c61&photoset_id=72157689340819233&format=json&nojsoncallback=1/')
        .then(response => {
            let photoData=response.data.photoset.photo;
            photoData.forEach(element => {
            let photoSource = "https://farm" + element.farm + ".staticflickr.com/" + element.server + "/" + element.id + "_" + element.secret + ".jpg";
            this.photoSources.push(photoSource);
            });
        })         
      }
  },

  created: function(){
    this.getPhotos();
  }
}
</script>

<style>
  @import './../Styles/Gallery.css';
</style>