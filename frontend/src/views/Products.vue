<template>
    <div>
      {{product.name}}
      {{$route.params.id}}
    </div>  
</template>
<script>
import  url from '../utils/api'
export default{
  name: 'Products',
  components:{
  },
  data(){
    return{
      product:{}
    }
  },
  computed:{},
    created(){
    fetch(url(`/products/${this.$route.params.id}`))
      .then(response => response.json())
      .then(data =>  {
        console.log(`data ${data}`)
        this.product = data
        } )    
  }}
</script>
