<template>
    <transition name="modal">
        <div class="modal-mask">
        <div class="modal-wrapper">
            <div class="modal-container">

            <div class="modal-header">
                <slot name="header">
                {{modalText.headerText}}
                </slot>
            </div>

            <div class="modal-body">
                <slot name="body">
                {{modalText.bodyText}}
                </slot>
            </div>

            <div class="modal-footer">
                <slot name="footer">
                <button class="btn btn-light" @click="$emit('close')">OK</button>
                </slot>
            </div>
            </div>
        </div>
        </div>
    </transition>
</template>

<script>
export default {
  name: 'Modal',
  components: {
  },  
  props: {
      modalText : Object
  }
}
</script>

<style scoped>
  @import './../Styles/Modal.css';
</style>