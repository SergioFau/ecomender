<template>
  <div class="home">
    <div>
      Listado de productos en el carrito
      
     <b-container class="bv-example-row">
            <div style="display:none">{{totalCart = 0}}</div>
        <b-row>
          <b-col sm="12" v-for="product in products" :key="product.id" style="display: flex">         
            <b-card-text style="width: 30%; margin-left: 15px; margin-right:15px; padding-top:15px">
              Nombre: {{product.name}}
            </b-card-text>
            <b-card-text style="width: 30%; margin-left: 15px; margin-right:15px; padding-top:15px">
              Precio: {{product.price}} | 
              Descuento: {{product.disccount}}%
            </b-card-text>
            <b-card-text style="display:none; width: 20%; margin-left: 15px; margin-right:15px; padding-top:15px">
              Precio Unidad: {{total = product.price - (product.price * product.disccount / 100)}}
            </b-card-text>

              <input disabled style="margin-top:15px; width: 10%;height: 50%; text-align:right" class="totales" type="number" v-bind:id="'numeroAux' + product.id" v-model="total">

             <b-card-text style="width: 20%; margin-left: 15px; margin-right:15px; padding-top:15px">
              <input style="width:35%; text-align:right" type="number" value=1 v-bind:id="'cantidad' + product.id" disabled>
                <b-button style="margin-right: 10px;margin-left: 10px;" v-on:click="incrementar(product.id)">+</b-button>
                <b-button v-on:click="decrementar(product.id)">-</b-button>
            </b-card-text>
            <div class="mt-2" style="width: 20%; padding-top:10px">
              <b-button  v-on:click="removeProduct(product)">X</b-button>
            </div>            
            <br>
            <div style="display:none">{{totalCart += total}}</div>
          </b-col>
        </b-row>       
            <div style="display:none">Total: {{totalCart}}</div>             
            Total: <input style="margin-top:10px; width: 7%; text-align:right" type="number" disabled v-bind:id="'totalCart'" v-model="totalCart">
        <b-button variant="primary" v-if="products.length > 0" v-on:click="removeCart(totalCart, products)">Finalizar Compra</b-button>
      </b-container>
    </div>
  </div>
</template>
<script>
import url from '../utils/api'
export default {
  name: 'Cart',
  components: {
  },
  data(){
    return {
      products: [],
      page: 0,
      perPage: 3,
      currentPage: 1,
      items: []
    }
  },
    // para tener el nombre en la url 
  created(){
    fetch(url(`/cart`))
      .then(response => response.json())
      .then(data =>  {
        console.log(`data ${data}`)
        this.products = data
        })    
  },
  methods:{   
      removeProduct(product){
      let data = {id: product.id};
      fetch(url(`/cart/`+product.id), {
        method: 'DELETE',
        headers:{
          'Content-type': 'application/json'
        },
        body: JSON.stringify(data)
      })
      alert("Producto Eliminado")
      location.reload();
    },
      removeCart(totalCart, products){
    let data = {};
            for(let i = 0; i < products.length; i++) {
                let product = products[i];
                data = {id: product.id};
                fetch(url(`/cart/`+product.id), {
                    method: 'DELETE',
                    headers:{
                    'Content-type': 'application/json'
                    },
                    body: JSON.stringify(data)
                })
            }
    alert("Productos Comprados")
    alert("Has Pagado un total de: "+ document.getElementById("totalCart").value)
      location.reload(); } ,    
    incrementar(id){
          let total = document.getElementById("numeroAux"+ id).value;

            if(document.getElementById("cantidad"+ id).value < 10){
                let x = document.getElementById("cantidad"+ id).value;
            let unidad = total /  document.getElementById("cantidad"+ id).value;
            document.getElementById("cantidad"+ id).value =  parseInt(x) + 1;

          total = unidad * document.getElementById("cantidad"+ id).value;

           document.getElementById("numeroAux"+ id).value = total;
           let totales = document.getElementsByClassName("totales");
            let totalInsertar = parseInt(0);
            for(let i = 0; i < totales.length; i++) {
                totalInsertar = totalInsertar + parseInt(totales[i].value);
            }
            document.getElementById("totalCart").value = parseInt(totalInsertar).toFixed(2);
            }       
    },      
    decrementar(id){
          let total = document.getElementById("numeroAux"+ id).value;

            if(document.getElementById("cantidad"+ id).value > 1){
                let x = document.getElementById("cantidad"+ id).value;
            let unidad = total /  document.getElementById("cantidad"+ id).value;
            document.getElementById("cantidad"+ id).value =  parseInt(x) - 1;

          total = unidad * document.getElementById("cantidad"+ id).value;

           document.getElementById("numeroAux"+ id).value = total;
           let totales = document.getElementsByClassName("totales");
            let totalInsertar = parseInt(0);
            for(let i = 0; i < totales.length; i++) {
                totalInsertar = totalInsertar + parseInt(totales[i].value);
            }
            document.getElementById("totalCart").value = parseInt(totalInsertar).toFixed(2);
            }       
    }
    },
  computed: {
    rows(){
      return this.items.length
    }
  }
} 
</script>