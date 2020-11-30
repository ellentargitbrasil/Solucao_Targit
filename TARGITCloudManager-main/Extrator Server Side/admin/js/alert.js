var formulario = document.getElementById('formulario');
var cnpj;
var razao;
var dias;
var horario;
var usuario;

formulario.addEventListener("submit", function(e){
    e.preventDefault();
    
    cnpj = document.getElementById('cnpj').value;
    razao = document.getElementById('razao').value;
    dias = document.getElementById('dias').value;
    horario = document.getElementById('horario').value;
    usuario = document.getElementById('usuario').value;

    if(cnpj == "" || cnpj.length < 14){
        // alert('Insira um cnpj válido')
        document.getElementById('cnpj').style.borderColor = "red";
        document.getElementById('cnpj').style.borderWidth = 2;
        return;
    }
    else if(razao == "" || razao.length <5){
        alert('Inválido')
        return;
    }
    else if(dias ==""){
        alert('Insira um dia')
        return;
    }
    else if(horario ==""){
        alert('Insira um horário')
        return;
    }
    else if(usuario =="" ){
        alert('Insira um usuário')
        return;
    }
  
})
    
