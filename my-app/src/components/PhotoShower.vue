<template>
  <div  class="pr" v-on:click="test">
      <img v-bind:src="trainer.photo">
      <p>{{trainer.fullName}}</p>
  </div>
</template>

<script>

export default {
  name: 'PhotoShower',
  components: {
  }, 

  methods: {
    test: function(){
      this.$emit('clicked', this.trainer.id);
    }
  },
  props: {
    trainer: Object
  },
}
</script>

<style scoped>
  @import './../Styles/PhotoShower.css';
</style>