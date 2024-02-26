export const navigation = [
  {
    text: 'Home',
    path: '/home',
    icon: 'home'
  },
  {
    text: 'Payments',
    icon: 'money',
    items: [
      {
        text: 'Payment history',
        path: '/payment-history'
      },
      {
        text: 'Next payment',
        path: '/payment-next'
      }
    ]
  },
  {
    text: 'Projects',
    icon: 'fields',
    items: [
      {
        text: 'Project budgets',
        path: '/project-budgets'
      },
      {
        text: 'Projects history',
        path: '/projects-history'
      }
      ,
      {
        text: 'Undone projects',
        path: '/projects-undone'
      }
    ]
  },
  {
    text: 'Import',
    path: '/import',
    icon: 'import'
  },
  {
    text: 'Team',
    path: '/team',
    icon: 'group'
  }
];
